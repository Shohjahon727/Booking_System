using Booking.Application.Models.User;
using Booking.Domain.Entities;
using Booking.Domain.Enum;
using Booking.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly BookingDbContext _context;

		public UsersController(BookingDbContext dbContext)
		{
			_context = dbContext;
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromForm] CreateUserRequestModel model)
		{
			var email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim();
			var phone = string.IsNullOrWhiteSpace(model.PhoneNumber) ? null : model.PhoneNumber.Trim();

			// Unique faqat qiymat berilganda — bo'sh email/phone "already exists" bermaydi
			if (email is not null &&
				await _context.Users.AnyAsync(u => u.Email == email))
			{
				return BadRequest(new { Message = "User with this email already exists" });
			}

			if (phone is not null &&
				await _context.Users.AnyAsync(u => u.PhoneNumber == phone))
			{
				return BadRequest(new { Message = "User with this phone number already exists" });
			}

			var passwordHash = Convert.ToBase64String(
				System.Text.Encoding.UTF8.GetBytes(model.Password));
			var user = new User
			{
				Id = Guid.NewGuid(),
				CreatedAt = DateTime.UtcNow,
				FullName = model.Name,
				Email = email ?? string.Empty,
				PhoneNumber = phone ?? string.Empty,
				PasswordHash = passwordHash,
				Role = model.Role
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			var result = new
			{
				Id = user.Id,
				FullName = user.FullName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Role = user.Role.ToString()
			};
			return Ok(new { Message = "User created successfully", result });
		}

		
		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _context.Users.AsNoTracking().Select(u => new {
				u.Id,
				u.FullName,
				u.Email,
				u.PhoneNumber,
				Role = u.Role.ToString()
			}).ToListAsync();

			return Ok(users);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
			if (user is null) return NotFound();

			_context.Users.Remove(user);

			await _context.SaveChangesAsync();

			return Ok(new { Message = "User deleted successfully" });
		}
	}
}
