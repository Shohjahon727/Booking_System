using Booking.Application.Common;
using Booking.Application.Models.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Services
{
	public interface IBookingService
	{
		Task<ServiceResult<object>> CreateAsync(Guid userId, CreateBookingRequestModel model, CancellationToken ct);
	}
}
