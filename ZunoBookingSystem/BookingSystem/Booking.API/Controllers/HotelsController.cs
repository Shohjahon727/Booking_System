using Booking.Application.Models.Hotel;
using Booking.Domain.Entities;
using Booking.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HotelsController : ControllerBase
	{
		private readonly BookingDbContext _context;

		public HotelsController(BookingDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetHotels(CancellationToken cancellationToken)
		{
			var hotels = await _context.Hotels
				.AsNoTracking()
				.OrderByDescending(h => h.CreatedAt)
				.Select(h => new
				{
					h.Id,
					h.Name,
					h.City,
					h.Address,
					h.Description,
					h.PhoneNumber,
					h.Email,
					h.ImageUrl,
					h.StarCount,
					h.Rating,
					h.Status,
					h.UserId,
					h.CreatedAt,
					h.UpdatedAt,

					Rooms = h.Rooms.Select(room => new
					{
						room.Id,
						room.Name,
						room.RoomType,
						room.RoomNumber,
						room.Capacity,
						room.PricePerNight,
						room.Description,
						room.ImageUrl,
						room.Status,
						room.HotelId,
						room.CreatedAt,
						room.UpdatedAt
					}).ToList()
				})
				.ToListAsync(cancellationToken);

			return Ok(hotels);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetHotelById(Guid id, CancellationToken cancellationToken)
		{
			var hotel = await _context.Hotels.AsNoTracking()
				.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

			if (hotel == null)
			{
				return NotFound();
			}

			return Ok(hotel);
		}

		[HttpPost]
		public async Task<IActionResult> CreateHotel([FromForm] CreateHotelRequest request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(request.Name) ||
				string.IsNullOrWhiteSpace(request.City))
			{
				return BadRequest("Name va City majburiy.");
			}

			if (request.OwnerId == Guid.Empty)
			{
				return BadRequest("OwnerId majburiy.");
			}

			var ownerExists = await _context.Users
				.AnyAsync(u => u.Id == request.OwnerId, cancellationToken);

			if (!ownerExists)
			{
				return BadRequest($"User topilmadi: {request.OwnerId}");
			}

			var hotel = new Hotel
			{
				Id = Guid.NewGuid(),
				UserId = request.OwnerId,
				Name = request.Name,
				City = request.City,
				Address = request.Address,
				Description = request.Description ?? string.Empty,
				ImageUrl = request.ImageUrl ?? string.Empty,
				StarCount = request.StarCount,
				PhoneNumber = request.PhoneNumber ?? string.Empty,
				Email = request.Email ?? string.Empty,
				CreatedAt = DateTime.UtcNow,
			};

			_context.Hotels.Add(hotel);
			await _context.SaveChangesAsync(cancellationToken);
			return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
		}
	}
}
