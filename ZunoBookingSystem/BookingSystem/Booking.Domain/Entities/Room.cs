using Booking.Domain.Common;
using Booking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Domain.Entities
{
	public class Room : Auditable
	{
		public string Name { get; set; } = string.Empty;
		public string RoomType { get; set; } = string.Empty;
		public string RoomNumber { get; set; } = string.Empty;
		public int Capacity { get; set; }
		public decimal PricePerNight { get; set; }
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
		public RoomStatus Status { get; set; }
		public Guid HotelId { get; set; }
		public Hotel? Hotel { get; set; }
	}
}
