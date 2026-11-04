using Booking.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking.API.Services
{
	public class JwtTokenService
	{
		private readonly IConfiguration _configuration;

		public JwtTokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public (string Token, DateTime ExpiresAt) CreateToken(User user)
		{
			var key = _configuration["Jwt:Key"]!;
			var issuer = _configuration["Jwt:Issuer"];
			var audience = _configuration["Jwt:Audience"];
			var expiresDays = int.Parse(_configuration["Jwt:ExpiresDays"] ?? "7");
			var expiresAt = DateTime.UtcNow.AddDays(expiresDays);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			};

			if (!string.IsNullOrWhiteSpace(user.Email))
				claims.Add(new Claim(ClaimTypes.Email, user.Email));

			if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
				claims.Add(new Claim("phone", user.PhoneNumber));

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
			var credentails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var jwt = new JwtSecurityToken(
					issuer: issuer,
					audience: audience,
					claims: claims,
					expires: expiresAt,
					signingCredentials: credentails
				);

			var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

			return (tokenString, expiresAt);
		}

		
	}
}
