using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Models.Booking
{
	public class CreateBookingRequestModel
	{
		public Guid RoomId { get; set; }
		public DateTime CheckInDate { get; set; }
		public DateTime CheckOutDate { get; set; }
		public int GuestCount { get; set; }
	}
}
