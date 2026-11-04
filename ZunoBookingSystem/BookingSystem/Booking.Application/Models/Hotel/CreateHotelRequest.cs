using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Models.Hotel
{
	public class CreateHotelRequest
	{
		public Guid OwnerId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
		public int StarCount { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }
	}
}
