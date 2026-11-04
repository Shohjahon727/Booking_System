using Booking.Domain.Common;
using Booking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Domain.Entities
{
	public class User : Auditable
	{
		public string FullName { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public UserRole Role { get; set; }

		public ICollection<UserBooking> Bookings { get; set; } = new List<UserBooking>();
		public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
	}
}
