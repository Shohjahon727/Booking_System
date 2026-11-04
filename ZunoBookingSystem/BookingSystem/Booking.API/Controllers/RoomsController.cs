using Booking.Application.Models.Room;
using Booking.Domain.Entities;
using Booking.Domain.Enum;
using Booking.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RoomsController : ControllerBase
	{
		private readonly BookingDbContext _context;
		public RoomsController(BookingDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetRooms(CancellationToken ct)
		{
			var rooms = await _context.Rooms
				.AsNoTracking()
				.OrderByDescending(r => r.CreatedAt)
				.Select(r => new
				{
					r.Id,
					r.Name,
					r.RoomType,
					r.RoomNumber,
					r.Capacity,
					r.PricePerNight,
					r.Description,
					r.ImageUrl,
					r.Status,
					r.HotelId,
					r.CreatedAt,
					r.UpdatedAt,

					Hotel = r.Hotel == null ? null : new
					{
						r.Hotel.Id,
						r.Hotel.Name,
						r.Hotel.City,
						r.Hotel.Address,
						r.Hotel.Description,
						r.Hotel.PhoneNumber,
						r.Hotel.Email,
						r.Hotel.ImageUrl,
						r.Hotel.StarCount,
						r.Hotel.Rating,
						r.Hotel.Status,
						r.Hotel.UserId,
						r.Hotel.CreatedAt,
						r.Hotel.UpdatedAt
					}
				})
				.ToListAsync(ct);

			return Ok(rooms);
		}

		[HttpPost]
		public async Task<IActionResult> CreateRoom([FromForm]CreateRoomRequestModel model,CancellationToken ct)
		{
			var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == model.HotelId, ct);

			if (hotel == null)
				return NotFound(new { message = "Ko'rsatilgan ID raqamidagi mehmonxona topilmadi." });

			if (hotel.Name == null)
				return NotFound();

			var room = new Room 
			{
				Id = Guid.NewGuid(),
				HotelId = model.HotelId,
				Name = model.Name,
				RoomNumber = model.RoomNumber,
				RoomType = model.RoomType,
				Capacity = model.Capacity,
				PricePerNight = model.PricePerNight,
				Description = model.Description,
				ImageUrl = model.ImageUrl,
				CreatedAt = DateTime.UtcNow
			};

			_context.Rooms.Add(room);
			await _context.SaveChangesAsync(ct);

			var result = new {
				Id = room.Id,
				HotelId =  room.HotelId,
				HotelName = hotel.Name,
				RoomNumber = room.RoomNumber,
				RoomType = room.RoomType,
				Capacity = room.Capacity,
				PricePerNight = room.PricePerNight,
				Description = room.Description,
				ImageUrl = room.ImageUrl,
				CreatedAt = room.CreatedAt
			};

			return Ok(result);

		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id,CancellationToken ct)
		{
			var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id, ct);
			return Ok(room);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRoom(Guid id,CancellationToken ct)
		{
			var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id,ct);
			if (room is null) return NotFound();

			_context.Rooms.Remove(room);
			await _context.SaveChangesAsync(ct);
			return Ok("Room delete done");
		}
	}
}
