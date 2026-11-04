using Booking.Domain.Common;
using Booking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Domain.Entities
{
	public class Hotel : Auditable
	{
		public string Name { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
		public int StarCount { get; set; }
		public decimal Rating { get; set; } = 0;
		public HotelStatus Status { get; set; } = HotelStatus.Pending;

		public Guid UserId { get; set; }
		public User? User { get; set; }

		public ICollection<Room> Rooms { get; set; } = new List<Room>();
	}
}
