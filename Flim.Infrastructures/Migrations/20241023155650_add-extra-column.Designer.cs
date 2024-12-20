﻿// <auto-generated />
using System;
using Flim.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Flim.Infrastructures.Migrations
{
    [DbContext(typeof(BookingDbContext))]
    [Migration("20241023155650_add-extra-column")]
    partial class addextracolumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Flim.Domain.Entities.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BookingId"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SlotId")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("numeric");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("BookingId");

                    b.HasIndex("SlotId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Flim.Domain.Entities.BookingSeat", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("integer");

                    b.Property<int>("SeatId")
                        .HasColumnType("integer");

                    b.HasKey("BookingId", "SeatId");

                    b.HasIndex("SeatId");

                    b.ToTable("BookingSeat");
                });

            modelBuilder.Entity("Flim.Domain.Entities.Film", b =>
                {
                    b.Property<int>("FilmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FilmId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("FilmId");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("Flim.Domain.Entities.HeldTicket", b =>
                {
                    b.Property<int>("HeldTicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("HeldTicketId"));

                    b.Property<int>("FilmId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("HoldExpiration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SeatId")
                        .HasColumnType("integer");

                    b.Property<int>("SlotId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("HeldTicketId");

                    b.HasIndex("FilmId");

                    b.HasIndex("SeatId");

                    b.HasIndex("SlotId");

                    b.HasIndex("UserId");

                    b.ToTable("HeldTickets");
                });

            modelBuilder.Entity("Flim.Domain.Entities.Seat", b =>
                {
                    b.Property<int>("SeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SeatId"));

                    b.Property<bool>("IsReserved")
                        .HasColumnType("boolean");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<string>("Row")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SlotId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("SeatId");

                    b.HasIndex("SlotId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("Flim.Domain.Entities.Slot", b =>
                {
                    b.Property<int>("SlotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SlotId"));

                    b.Property<int>("FilmId")
                        .HasColumnType("integer");

                    b.Property<int>("ShowCategory")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("SlotDate")
                        .HasColumnType("date");

                    b.HasKey("SlotId");

                    b.HasIndex("FilmId");

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("Flim.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Flim.Domain.Entities.Booking", b =>
                {
                    b.HasOne("Flim.Domain.Entities.Slot", "Slot")
                        .WithMany("Bookings")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flim.Domain.Entities.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Slot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Flim.Domain.Entities.BookingSeat", b =>
                {
                    b.HasOne("Flim.Domain.Entities.Booking", "Booking")
                        .WithMany("BookingSeats")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flim.Domain.Entities.Seat", "Seat")
                        .WithMany("BookingSeats")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("Flim.Domain.Entities.HeldTicket", b =>
                {
                    b.HasOne("Flim.Domain.Entities.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flim.Domain.Entities.Seat", "Seat")
                        .WithMany()
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flim.Domain.Entities.Slot", "Slot")
                        .WithMany()
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flim.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Seat");

                    b.Navigation("Slot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Flim.Domain.Entities.Seat", b =>
                {
                    b.HasOne("Flim.Domain.Entities.Slot", "Slot")
                        .WithMany("Seats")
                        .HasForeignKey("SlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Slot");
                });

            modelBuilder.Entity("Flim.Domain.Entities.Slot", b =>
                {
                    b.HasOne("Flim.Domain.Entities.Film", "Film")
                        .WithMany("Slots")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");
                });

            modelBuilder.Entity("Flim.Domain.Entities.Booking", b =>
                {
                    b.Navigation("BookingSeats");
                });

            modelBuilder.Entity("Flim.Domain.Entities.Film", b =>
                {
                    b.Navigation("Slots");
                });

            modelBuilder.Entity("Flim.Domain.Entities.Seat", b =>
                {
                    b.Navigation("BookingSeats");
                });

            modelBuilder.Entity("Flim.Domain.Entities.Slot", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Seats");
                });

            modelBuilder.Entity("Flim.Domain.Entities.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
