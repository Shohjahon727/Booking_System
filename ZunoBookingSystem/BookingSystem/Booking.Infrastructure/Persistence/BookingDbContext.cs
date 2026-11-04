using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Infrastructure.Persistence
{
	public class BookingDbContext : DbContext
	{
		public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
		{
		}

		public DbSet<User> Users => Set<User>();
		public DbSet<Hotel> Hotels => Set<Hotel>();
		public DbSet<Room> Rooms => Set<Room>();
		public DbSet<UserBooking> Bookings => Set<UserBooking>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	
	}
}
