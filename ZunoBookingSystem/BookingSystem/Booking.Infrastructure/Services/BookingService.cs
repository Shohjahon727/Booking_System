using Booking.Application.Common;
using Booking.Application.Models.Booking;
using Booking.Application.Services;
using Booking.Domain.Entities;
using Booking.Domain.Enum;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Booking.Infrastructure.Services
{
	public class BookingService : IBookingService
	{
		private readonly BookingDbContext _context;

		public BookingService(BookingDbContext context)
		{
			_context = context;
		}

		

		public async Task<ServiceResult<object>> CreateAsync(Guid userId, CreateBookingRequestModel model, CancellationToken ct)
		{
			if (model.RoomId == Guid.Empty)
				return ServiceResult<object>.Fail("RoomId is required.", 400);

			if (model.CheckInDate.Date < DateTime.UtcNow.Date)
				return ServiceResult<object>.Fail("check in date cannot be past.", 400);

			if (model.CheckOutDate.Date <= model.CheckInDate.Date)
				return ServiceResult<object>.Fail("Check out date be after check-in date.", 400);

			var userExists = await _context.Users.AnyAsync(u => u.Id == userId, ct);

			if (!userExists)
				return ServiceResult<object>.Fail("User not found.", 404);

			var room = await _context.Rooms.AsNoTracking()
				.Where(r => r.Id == model.RoomId)
				.Select(r => new
				{
					r.Id,
					r.Capacity,
					r.PricePerNight,
					r.Status,
					OwerId = r.Hotel.UserId
				}).FirstOrDefaultAsync(ct);

			if (room is null)
				return ServiceResult<object>.Fail("Room not found.", 404);

			if (room.OwerId == userId)
				return ServiceResult<object>.Fail("You cannot book your own room.", 403);

			if (room.Status != RoomStatus.Active)
				return ServiceResult<object>.Fail("Room is not avaible.", 400);

			if (room.Capacity < model.GuestCount)
				return ServiceResult<object>.Fail("Gues count exceeds room capacity",400);

			var checkInDate = DateTime.SpecifyKind(model.CheckInDate.Date,DateTimeKind.Utc);
			var checkOutDate = DateTime.SpecifyKind(model.CheckOutDate.Date,DateTimeKind.Utc);

			var isBooked = await _context.Bookings.AnyAsync(b =>
				b.RoomId == model.RoomId &&
				b.CheckInDate < checkOutDate &&
				b.CheckOutDate > checkInDate &&
				(b.Status == BookingStatus.Pending || b.Status == BookingStatus.Confirmed), ct
			);

			if (isBooked)
				return ServiceResult<object>.Fail("Room is already booked for the selected dates.", 400);

			var nights = (checkOutDate - checkInDate).Days;
			var totalPrice = nights * room.PricePerNight;

			var booking = new UserBooking
			{
				Id = Guid.NewGuid(),
				UserId = userId,
				RoomId = model.RoomId,
				CheckInDate = checkInDate,
				CheckOutDate = checkOutDate,
				NumberOfGuests = model.GuestCount,
				TotalPrice = totalPrice,
				Status = BookingStatus.Pending,
				CreatedAt = DateTime.UtcNow
			};

			_context.Bookings.Add(booking);
			await _context.SaveChangesAsync(ct);

			var result = new
			{
				Message = "Booking created successfully.",
				BookingID = booking.Id,
				booking.UserId,
				booking.RoomId,
				booking.CheckInDate,
				booking.CheckOutDate,
				booking.NumberOfGuests,
				booking.TotalPrice,
				booking.Status,
			};

			return ServiceResult<object>.Ok(result);
		}

		public async Task<ServiceResult<object>> CancelAsync(Guid userId, Guid bookingId, string role, CancellationToken ct)
		{
			var booking = _context.Bookings.FirstOrDefault(b => b.Id == bookingId);

			if (booking == null)
				return ServiceResult<object>.Fail("Booking not found.", 404);

			var isAuthorized = role switch
			{
				"Admin" => true,
				"User" => booking.UserId == userId,
				"Hotel" => booking.Room.Hotel.UserId == userId,
				_ => false
			};

			if (!isAuthorized)
				return ServiceResult<object>.Fail("You do not have permission to cancel this booking.", 403);

			if (booking.Status != BookingStatus.Pending &&
					booking.Status != BookingStatus.Confirmed)
			{
				return ServiceResult<object>.Fail(
					"Only pending or confirmed bookings can be cancelled.", 400);
			}

			booking.Status = BookingStatus.Cancelled;
			booking.UpdatedAt = DateTime.UtcNow;

			await _context.SaveChangesAsync(ct);

			var data = new
			{
				Message = "Booking cancelled successfully.",
				BookingId = booking.Id,
				booking.Status,
				booking.UpdatedAt
			};

			return ServiceResult<object>.Ok(data);
		}

		public async Task<ServiceResult<object>> ConfirmAsync(Guid userId, Guid bookingId, string role, CancellationToken ct)
		{
			var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == bookingId, ct);

			if (booking == null)
				return ServiceResult<object>.Fail("Booking not found.", 404);

			if (role == "Hotel")
			{
				var ownsBooking = await _context.Bookings.AnyAsync(
					b => b.Id == bookingId && b.Room.Hotel.UserId == userId, ct);
				if (!ownsBooking)
					return ServiceResult<object>.Fail("You do not have permission to confirm this booking.", 403);

				if (booking.Status != BookingStatus.Pending)
					return ServiceResult<object>.Fail("Only pending bookings can be confirmed.", 400);

			}
			booking.Status = BookingStatus.Confirmed;
			booking.UpdatedAt = DateTime.UtcNow;

			await _context.SaveChangesAsync(ct);

			var data = new
			{
				Message = "Booking confirmed successfully.",
				BookingId = booking.Id,
				booking.Status,
				booking.UpdatedAt
			};

			return ServiceResult<object>.Ok(data);
		}

		public async Task<ServiceResult<object>> GetListAsyc(Guid userId, string role, CancellationToken ct)
		{
			var query = _context.Bookings.AsNoTracking().AsQueryable();

			query = role switch
			{
				"Admin" => query,
				"User" => query.Where(x => x.UserId == userId),
				"Hotel" => query.Where(x => x.Room.Hotel.UserId == userId),
				_ => null!
			};

			if (query == null)
				return ServiceResult<object>.Fail("You do not have permission to view bookings.", 403);

			var bookings = await query.OrderByDescending(x => x.CreatedAt)
				.Select(x => new
				{
					x.Id,
					x.UserId,
					x.RoomId,
					x.CheckInDate,
					x.CheckOutDate,
					x.NumberOfGuests,
					x.TotalPrice,
					x.Status,
					x.CreatedAt
				}).ToListAsync(ct);

			return ServiceResult<object>.Ok(bookings);
		}
		public async Task<ServiceResult<object>> GetByIdAsync(Guid userId, Guid bookingId, string role, CancellationToken ct)
		{
			var booking = await _context.Bookings.AsNoTracking()
				.Where(x => x.Id == bookingId)
				.Select(x => new
				{
					x.Id,
					x.UserId,
					x.RoomId,
					x.CheckInDate,
					x.CheckOutDate,
					x.NumberOfGuests,
					x.TotalPrice,
					x.Status,
					x.CreatedAt
				}).FirstOrDefaultAsync(ct);

			if (booking is null)
				return ServiceResult<object>.Fail("Booking not found.", 404);

			var isAuthorized = role switch
			{
				"Admin" => true,
				"User" => booking.UserId == userId,
				"Hotel" => await _context.Bookings.AnyAsync(b => b.Id == bookingId && b.Room.Hotel.UserId == userId, ct),
				_ => false
			};

			if (!isAuthorized)
				return ServiceResult<object>.Fail("You do not have permission to view this booking.", 403);

			return ServiceResult<object>.Ok(booking);
		}

		public async Task<ServiceResult<object>> DeleteAsync(Guid userId, Guid bookingId, string role, CancellationToken ct)
		{
			var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == bookingId, ct);
			if (booking == null)
				return ServiceResult<object>.Fail("Booking not found.", 404);

			var isAuthorized = role switch
			{
				"Admin" => true,
				"User" => booking.UserId == userId,
				"Hotel" => booking.Room.Hotel.UserId == userId,
				_ => false
			};
			if (!isAuthorized)
				return ServiceResult<object>.Fail("You do not have permission to delete this booking.", 403);

			_context.Bookings.Remove(booking);
			await _context.SaveChangesAsync(ct);

			return ServiceResult<object>.Ok(new { Message = "Booking deleted successfully." });
		}
	}
}
