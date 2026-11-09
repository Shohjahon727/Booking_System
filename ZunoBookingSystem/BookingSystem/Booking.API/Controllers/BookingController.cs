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
		public async Task<IActionResult> GetBooking(CancellationToken ct)
		{
			var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var role = User.FindFirstValue(ClaimTypes.Role);

			if (!Guid.TryParse(userIdValue, out var userId))
				return Unauthorized("Invalid user token");

			if (string.IsNullOrWhiteSpace(role))
				return Unauthorized("Invalid role");

			var result = await _service.GetListAsyc(userId, role, ct);

			if (!result.Success)
			{
				return result.StatusCode switch
				{
					403 => Forbid(),
					_ => StatusCode(result.StatusCode, new { Message = result.Error })
				};
			}

			return Ok(result.Data);
		}

		[Authorize]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdBooking(Guid id,CancellationToken ct)
		{
			var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var role = User.FindFirstValue(ClaimTypes.Role);

			if (!Guid.TryParse(userIdValue, out var userId))
				return Unauthorized();

			if (string.IsNullOrWhiteSpace(role))
				return Unauthorized("Invalid role");

			var result = await _service.GetByIdAsync(userId, id, role, ct);

			if (!result.Success)
			{
				return result.StatusCode switch
				{
					403 => Forbid(),
					404 => NotFound(new { Message = result.Error }),
					_ => StatusCode(result.StatusCode, new { Message = result.Error })
				};
			}

			return Ok(result.Data);
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

			if (string.IsNullOrWhiteSpace(role))
				return Unauthorized("Invalid role");

			var result = await _service.ConfirmAsync(userId, id, role, ct);
			
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

		[Authorize]
		[HttpPatch("{id}/cancel")]
		public async Task<IActionResult> CancelBooking(Guid id,CancellationToken ct)
		{
			var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var role = User.FindFirstValue(ClaimTypes.Role);

			if (!Guid.TryParse(userIdValue, out var userId))
				return Unauthorized("Invalid token user");

			if (string.IsNullOrWhiteSpace(role))
				return Unauthorized("Invalid role");

			var result = await _service.CancelAsync(userId, id, role, ct);

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

		[Authorize(Roles = "Admin,Hotel")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id , CancellationToken ct)
		{
			var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var role = User.FindFirstValue(ClaimTypes.Role);

			if (!Guid.TryParse(userIdValue, out var userId))
				return Unauthorized("Invalid token user");

			if (string.IsNullOrWhiteSpace(role))
				return Unauthorized("Invalid role");
			var result = await _service.DeleteAsync(userId,id, role,ct);

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
	}
}
