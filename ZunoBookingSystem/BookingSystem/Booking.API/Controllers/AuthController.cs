using BCrypt.Net;
using Booking.API.Services;
using Booking.Application.Models.Auth;
using Booking.Domain.Entities;
using Booking.Domain.Enum;
using Booking.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly BookingDbContext _context;
		private readonly JwtTokenService _service;
		private readonly IConfiguration _configuration;

		public AuthController(BookingDbContext context,JwtTokenService service, IConfiguration configuration)
		{
			_context = context;
			_service = service;
			_configuration = configuration;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromForm]RegisterRequestModel model,CancellationToken ct)
		{
			if (string.IsNullOrWhiteSpace(model.FullName) ||
				string.IsNullOrWhiteSpace(model.Password))
			{
				return BadRequest(new { Message = "FullName va Password majburiy." });
			}

			var email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim();
			var phone = string.IsNullOrWhiteSpace(model.PhoneNumber) ? null : model.PhoneNumber.Trim();

			if (email is null && phone is null)
			{
				return BadRequest(new { Message = "Email yoki PhoneNumber dan bittasi majburiy." });
			}

			if (email is not null &&
				await _context.Users.AnyAsync(u => u.Email == email, ct))
			{
				return BadRequest(new { Message = "Bu email band." });
			}
			if (phone is not null &&
				await _context.Users.AnyAsync(u => u.PhoneNumber == phone, ct))
			{
				return BadRequest(new { Message = "Bu telefon band." });
			}
			if (model.Role == UserRole.Admin)
				return BadRequest("Admin sifatida royxatdan otib bolmaydi");

			var user = new User
			{
				Id = Guid.NewGuid(),
				FullName = model.FullName.Trim(),
				Email = email ?? string.Empty,
				PhoneNumber = phone ?? string.Empty,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
				Role = model.Role,
				CreatedAt = DateTime.UtcNow
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync(ct);

			var result = new
			{
				Message = "User registered successfully",
				User = new
				{
					user.Id,
					user.FullName,
					Email = string.IsNullOrEmpty(user.Email) ? null : user.Email,
					Phone = string.IsNullOrEmpty(user.PhoneNumber) ? null : user.PhoneNumber,
					Role = user.Role.ToString()
				}
			};

			return Ok(result);


		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromForm]LoginRequestModel model, CancellationToken ct)
		{
			if (string.IsNullOrWhiteSpace(model.Password))
			{
				return BadRequest(new { Message = "Password majburiy." });
			}
			var email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim();
			var phone = string.IsNullOrWhiteSpace(model.PhoneNumber) ? null : model.PhoneNumber.Trim();

			if (email is null && phone is null)
			{
				return BadRequest(new { Message = "Email yoki PhoneNumber dan bittasi kerak." });
			}

			User? user = null;

			if (email is not null)
				user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
			else
				user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone, ct);

			if (user is null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
				return Unauthorized("Login yoki parol xato");

			var (token, expiresAt) = _service.CreateToken(user);

			var result = new
			{
				Token = token,
				ExpiresAt = expiresAt,
				User = new
				{
					user.Id,
					user.FullName,
					Email = string.IsNullOrEmpty(user.Email) ? null : user.Email,
					Role = user.Role.ToString()
				}
			};
			return Ok(result);
		}

		[Authorize]
		[HttpGet("me")]
		public async Task<IActionResult> GetCurrentUser(CancellationToken token)
		{
			var userValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (!Guid.TryParse(userValue, out var userId))
				return Unauthorized("Invalid user token.");

			var user = await _context.Users.AsNoTracking().Where(u => u.Id == userId)
				.Select(u => new
				{
					u.Id,
					u.FullName,
					u.Email,
					u.PhoneNumber,
					Role = u.Role.ToString()
				}).FirstOrDefaultAsync();

			if (user == null)
				return NotFound("User not found");

			return Ok(user);
		}

	}
}
