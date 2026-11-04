using Booking.Domain.Common;
using Booking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Domain.Entities
{
	public class UserBooking : Auditable
	{
		public DateTime CheckInDate { get; set; }
		public DateTime CheckOutDate { get; set; }
		public int NumberOfGuests { get; set; }
		public decimal TotalPrice { get; set; }
		public BookingStatus Status { get; set; }

		public Guid UserId { get; set; }
		public Guid RoomId { get; set; }
		public User? User { get; set; }
		public Room? Room { get; set; }


		public void ValidateBasics()
		{
			if (CheckInDate >= CheckOutDate)
			{
				throw new ArgumentException("Check-in date must be before check-out date.");
			}

			if (NumberOfGuests <= 0)
			{
				throw new ArgumentException("Number of guests must be greater than zero.");
			}

			if (TotalPrice < 0)
			{
				throw new ArgumentException("Total price cannot be negative.");
			}
		}
	}

	
}
