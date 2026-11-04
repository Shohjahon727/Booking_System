using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Domain.Common
{
	public abstract class Auditable : BaseEntity
	{
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
