using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Models.Room
{
	public class CreateRoomRequestModel
	{
		public Guid HotelId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string RoomType { get; set; } = string.Empty;
		public string RoomNumber { get; set; } = string.Empty;
		public int Capacity { get; set; }
		public decimal PricePerNight { get; set; }
		public string Description { get; set; } = string.Empty;
		public string? ImageUrl { get; set; }
	}
}
