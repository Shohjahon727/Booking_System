using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Common
{
	public class ServiceResult<T>
	{
		public bool Success { get; set; }
		public string? Error { get; set; }
		public int StatusCode { get; set; }
		public T? Data { get; set; }

		public static ServiceResult<T> Ok(T data)
		{
			return new ServiceResult<T>
			{
				Success = true,
				StatusCode = 200,
				Data = data
			};
		}

		public static ServiceResult<T> Fail(string error, int statusCode)
		{
			return new ServiceResult<T>
			{
				Success = false,
				Error = error,
				StatusCode = statusCode
			};
		}
	}
}
