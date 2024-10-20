using Flim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Infrastructures.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

        public DbSet<Film> Films { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                      .ValueGeneratedOnAdd() // Ensures auto-increment.
                      .IsRequired();
            });

            modelBuilder.Entity<Booking>()
                        .HasOne(b => b.User)
                        .WithMany(u => u.Bookings)
                        .HasForeignKey(b => b.UserId);


            modelBuilder.Entity<Showtime>()
                .HasOne(s => s.Film)
                .WithMany(f => f.Showtimes)
                .HasForeignKey(s => s.FilmId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Showtime)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.ShowtimeId);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Showtime)
                .WithMany(st => st.Seats)
                .HasForeignKey(s => s.ShowtimeId);

            modelBuilder.Entity<BookingSeat>()
                .HasKey(bs => new { bs.BookingId, bs.SeatId });

            modelBuilder.Entity<BookingSeat>()
                .HasOne(bs => bs.Booking)
                .WithMany(b => b.BookingSeats)
                .HasForeignKey(bs => bs.BookingId);

            modelBuilder.Entity<BookingSeat>()
                .HasOne(bs => bs.Seat)
                .WithMany(s => s.BookingSeats)
                .HasForeignKey(bs => bs.SeatId);



            base.OnModelCreating(modelBuilder); 
        }



    }
}
