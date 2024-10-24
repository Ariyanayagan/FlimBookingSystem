using BCrypt.Net;
using Flim.Application.ApplicationException;
using Flim.Application.Interfaces;
using Flim.Application.Records;
using Flim.Domain.Entities;
using Flim.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
      
        public UserService(IGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        // Authenticate user using username and password
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var foundUser = await _userRepository.FindSingleAsync(u => u.Email == username);
            Debug.WriteLine($"Password Hash : {BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA256)}");
            if (foundUser != null && BCrypt.Net.BCrypt.Verify(password, foundUser.Password))
            {
                return foundUser; 
            }
            return null;
        }

        // Register a new user
        public async Task<bool> RegisterAsync(User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                await _userRepository.InsertAsync(user);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex) {

                await _unitOfWork.RollbackTransaction();
                await _unitOfWork.DisposeTransactionAsync();
                throw ex;
            }
            
        }


        public async Task<List<MyOrderRecord>> GetBookingsAsync(int id)
        {

            var myBookings = await _userRepository.GetAllAsync(include: user => user
                  .Include(u => u.Bookings)
                  .ThenInclude(b => b.Slot)
                  .ThenInclude(s => s.Film)
             );


            if (myBookings is null || myBookings.Count() == 0)
            {
                throw new NotFoundException("Bookings Not found for this user id - " + id.ToString());
            }

            var userBookings = myBookings.Where(user => user.UserId == id);

            var bookingDetails = userBookings.SelectMany(user => user.Bookings.Select(booking => new MyOrderRecord
            (
                MovieName: booking.Slot.Film.Name,
                Amount: booking.TotalCost,
                dateTIme: booking.BookingDate,
                ShowTime: booking.Slot.ShowCategory.ToString(),
                ShowTimeint: (int)booking.Slot.ShowCategory,
                SlotDate: booking.Slot.SlotDate,
                seats: getBookingSeats(booking.BookingId).GetAwaiter().GetResult()
            )));

            return bookingDetails.ToList();
        }

        public async Task<List<int>> getBookingSeats(int id)
        {
            var bookingSeats = await _unitOfWork.Repository<BookingSeat>().GetAllAsync(include: bs => bs.Include(s => s.Seat));

            return bookingSeats.Where(se => se.BookingId == id).Select(se => se.Seat.Number).ToList();

        }
    }
}
