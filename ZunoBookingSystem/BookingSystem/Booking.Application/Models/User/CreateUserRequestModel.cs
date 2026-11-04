using Booking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Models.User
{
	public class CreateUserRequestModel
	{
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public UserRole Role { get; set; } = UserRole.User;
	}
}
