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
        //public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<HeldTicket> HeldTickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                      .ValueGeneratedOnAdd() 
                      .IsRequired();
            });

            modelBuilder.Entity<Booking>()
                        .HasOne(b => b.User)
                        .WithMany(u => u.Bookings)
                        .HasForeignKey(b => b.UserId);




            modelBuilder.Entity<Slot>()
                .HasOne(s => s.Film)
                .WithMany(f => f.Slots) 
                .HasForeignKey(s => s.FilmId);



            modelBuilder.Entity<Booking>()
               .HasOne(b => b.Slot) 
               .WithMany(s => s.Bookings)
               .HasForeignKey(b => b.SlotId);


            modelBuilder.Entity<Seat>()
               .HasOne(s => s.Slot) // Updated to Slot
               .WithMany(st => st.Seats)
               .HasForeignKey(s => s.SlotId);


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

            modelBuilder.Entity<HeldTicket>()
                .HasOne(ht => ht.Film)
                .WithMany()
                .HasForeignKey(ht => ht.FilmId);

            modelBuilder.Entity<HeldTicket>()
                .HasOne(ht => ht.Slot)
                .WithMany()
                .HasForeignKey(ht => ht.SlotId);

            modelBuilder.Entity<HeldTicket>()
                .HasOne(ht => ht.Seat)
                .WithMany()
                .HasForeignKey(ht => ht.SeatId);

            //modelBuilder.Entity<HeldTicket>()
            //    .HasOne(ht => ht.User)
            //    .WithMany()
            //    .HasForeignKey(ht => ht.UserId);



            base.OnModelCreating(modelBuilder); 
        }



    }
}
