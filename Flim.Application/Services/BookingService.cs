using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Flim.Application.Records;
using Flim.Domain.Entities;
using Flim.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Services
{
    public class BookingService : IBookingService
    {
        public readonly IUnitOfWork _unitOfWork;
        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<FilmRecord>> GetAvailableSeats(int id, DateOnly date)
        {

            var slots = await _unitOfWork.Repository<Film>().GetAllAsync(include: slot => slot
                          .Include(fl => fl.Slots)
                          .ThenInclude(sl => sl.Seats));

            var result = slots
                .Where(fl => fl.FilmId == id)
                .Select(fl => new FilmRecord(
                    fl.FilmId,
                    fl.Name,
                    fl.Description,
                    fl.Genre,
                    fl.Duration,
                    fl.Slots.Where(sl=>sl.SlotDate == date)
                    .Select(slot => new SlotRecord(
                        slot.SlotId,
                        slot.ShowCategory.ToString(),
                        slot.Seats
                            .Where(seat => !seat.IsReserved)
                            .OrderBy(seat => seat.Number)
                            .Select(seat => new SeatRecord(
                                seat.SeatId,
                                seat.Row,
                                seat.Number
                            )).ToList()
                    )).ToList()
                )).ToList();


            return result;


        }

        public async Task<bool> HoldAsync(BookingDTO booking)
        {
            var book = await  _unitOfWork.Repository<Slot>().GetAllAsync(slot => slot.Include(sl => sl.Seats));

            var Totalseat = book.Where(se => se.FilmId == booking.FilmId 
                && se.SlotDate == booking.date
                && se.ShowCategory == booking.category

            )
            .ToList();

            var seats = Totalseat.Select(se=>se.Seats).ToList();

            return true;

        }


    }
}
