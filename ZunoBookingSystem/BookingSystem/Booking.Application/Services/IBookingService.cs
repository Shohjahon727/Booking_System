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
		Task<ServiceResult<object>> ConfirmAsync(Guid userId,Guid bookingId, string role, CancellationToken ct);
		Task<ServiceResult<object>> CancelAsync(Guid userId, Guid bookingId, string role, CancellationToken ct);
		Task<ServiceResult<object>> GetListAsyc(Guid userId, string role, CancellationToken ct);
		Task<ServiceResult<object>> GetByIdAsync(Guid userId, Guid bookingId, string role, CancellationToken ct);
		Task<ServiceResult<object>> DeleteAsync(Guid userId, Guid bookingId, string role, CancellationToken ct);
	}
}
