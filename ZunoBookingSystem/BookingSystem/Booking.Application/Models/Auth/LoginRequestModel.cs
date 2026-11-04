using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Models.Auth
{
	public class LoginRequestModel
	{
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string Password { get; set; }
	}
}
