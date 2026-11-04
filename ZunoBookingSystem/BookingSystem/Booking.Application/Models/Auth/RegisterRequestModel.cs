using Booking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Models.Auth
{
	public class RegisterRequestModel
	{
		public string FullName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string Password { get; set; }
		public UserRole Role { get; set; } = UserRole.User;
	}
}
