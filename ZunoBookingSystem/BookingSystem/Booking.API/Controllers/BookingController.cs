using Booking.Application.Models.Booking;
using Booking.Application.Services;
using Booking.Domain.Entities;
using Booking.Domain.Enum;
using Booking.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Booking.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BookingController : ControllerBase
	{
		private readonly BookingDbContext _context;
		private readonly IBookingService _service;
		public BookingController(BookingDbContext context, IBookingService service)
		{
			_context = context;
			_service = service;
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> GetBooking()
		{
			var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var role = User.FindFirstValue(ClaimTypes.Role);

			if (!Guid.TryParse(userIdValue, out var userId))
				return Unauthorized("Invalid user token");

			var query =  _context.Bookings.AsNoTracking().AsQueryable();

			query = role switch
			{
				"Admin" => query,
				"User" => query.Where(x => x.UserId == userId),
				"Hotel" => query.Where(x => x.Room.Hotel.UserId == userId),
				_ => null
			};

			if (query == null)
				return Forbid();

			var booking = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();

			return Ok(booking);
		}

		[Authorize]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdBooking(Guid id,CancellationToken ct)
		{
			var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var role = User.FindFirstValue(ClaimTypes.Role);

			if (!Guid.TryParse(userIdValue, out var userId))
				return Unauthorized();

			
			var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
			if (booking == null) return NotFound("Booking not found.");

			var isAuthorized = role switch
			{
				"Admin" => true,
				"User" => booking.UserId == userId,
				"Hotel" => await _context.Bookings.AnyAsync(b => b.Id == id && b.Room.Hotel.UserId == userId, ct),
				_ => false
			};

			if (!isAuthorized)
				return Forbid();



			return Ok(booking);
		}

		[Authorize(Roles = "User")]
		[HttpPost]
		public async Task<IActionResult> CreateBooking([FromForm]CreateBookingRequestModel model,CancellationToken ct)
		{
			var userIdValid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!Guid.TryParse(userIdValid, out var userId))
				return Unauthorized("Invalid token user");

			var result = await _service.CreateAsync(userId, model, ct);

			if (!result.Success)
			{
				return result.StatusCode switch
				{
					400 => BadRequest(new { Message = result.Error }),
					403 => Forbid(),
					404 => NotFound(new { Message = result.Error }),
					409 => Conflict(new { Message = result.Error }),
					_ => StatusCode(result.StatusCode, new { Message = result.Error })
				};
			}

			return Ok(result.Data);


		}

		[Authorize(Roles ="Hotel,Admin")]
		[HttpPatch("{id}/confirm")]
		public async Task<IActionResult> Confirm(Guid id,CancellationToken ct)
		{
			var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var role = User.FindFirstValue(ClaimTypes.Role);

			if (!Guid.TryParse(userIdValue, out var userId))
				return Unauthorized("Invalid token user");

			var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);

			if (booking is null) return NotFound("Booking not found");

			if (role == "Hotel")
			{
				var ownsBooking = await _context.Bookings.AnyAsync(b => b.Id == id && b.Room.Hotel.UserId == userId,ct);

				if (!ownsBooking)
					return Forbid();
			}

			if (booking.Status != BookingStatus.Pending) 
				return BadRequest("Only pending bookings can be confirmed.");

			booking.Status = BookingStatus.Confirmed;
			booking.UpdatedAt = DateTime.UtcNow;

			await _context.SaveChangesAsync(ct);

			var result = new
			{
				Message = "Booking confirmed successfully.",
				BookingId = booking.Id,
				booking.Status,
				booking.UpdatedAt
			};

			return Ok(result);

		}

		[Authorize]
		[HttpPatch("{id}/cancel")]
		public async Task<IActionResult> CancelBooking(Guid id,CancellationToken ct)
		{
			var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var role = User.FindFirstValue(ClaimTypes.Role);

			if (!Guid.TryParse(userIdValue, out var userId))
				return Unauthorized("Invalid token user");
			var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);

			if (booking is null) return NotFound("Booking not found.");

			var isAuthorized = role switch
			{
				"Admin" => true,
				"User" => booking.UserId == userId,
				"Hotel" => await _context.Bookings.AnyAsync(b => b.Id == id && b.Room.Hotel.UserId == userId, ct),
				_ => false
			};

			if (!isAuthorized)
				return Forbid();

			if (booking.Status != BookingStatus.Confirmed && booking.Status != BookingStatus.Pending)
				return BadRequest("Only pending or confirmed bookings can be cancelled.");

			booking.Status = BookingStatus.Cancelled;
			booking.UpdatedAt = DateTime.UtcNow;

			await _context.SaveChangesAsync(ct);

			var result = new {
				Message = "Booking canceled successfully.",
				BookingId = booking.Id,
				booking.Status,
				booking.UpdatedAt
			};

			return Ok(result);
		}

		[Authorize(Roles = "Admin,Hotel")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
			if (booking == null) return NotFound();

			_context.Bookings.Remove(booking);
			await _context.SaveChangesAsync();

			return Ok(booking);
		}
	}
}
