using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Flim.Application.Records;
using Flim.Domain.Entities;
using Flim.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;


namespace Flim.Application.Services
{
    public class BookingService : IBookingService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor httpContextAccess;

        public BookingService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext) 
        {
            _unitOfWork = unitOfWork;
            this.httpContextAccess = httpContext;
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
                            .Where(seat => seat.IsReserved == false)
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

        public async Task HoldAsync(BookingDTO booking)
        {
            try
            {
                var userId = httpContextAccess.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var totalSeats = await TotalSeats(booking);

                if ( totalSeats is null || !totalSeats.Any())
                {
                    throw new InvalidOperationException("No seats available");
                }

                if (totalSeats.Count != booking.SeatNumbers.Count())
                {
                    throw new InvalidOperationException("Ticket Already Booked!");
                }

                List<HeldTicket> heldTickets = new List<HeldTicket>();

                var repo = _unitOfWork.Repository<HeldTicket>();

                foreach (var seat in totalSeats)
                {
                    var heltseats = await HeldSeats(seat, booking.FilmId);

                    if (heltseats.Any())
                    {
                        throw new InvalidOperationException("Ticket already in hold!");
                    }
                    else
                    {
                        var entity = new HeldTicket
                        {
                            FilmId = booking.FilmId,
                            SeatId = seat.SeatId,
                            SlotId = seat.SlotId,
                            HoldExpiration = DateTime.UtcNow.AddMinutes(10), // Changed to 10 minutes
                            UserId = Convert.ToInt32(userId)
                        };

                        seat.IsReserved = true;

                        heldTickets.Add(entity);
                    }
                }
                

                try
                {
                    _unitOfWork.BeginTransaction();
                    await repo.InsertRangeAsync(heldTickets);
                    _unitOfWork.Repository<Seat>().UpdateRange(totalSeats);
                    await _unitOfWork.SaveAsync();
                    _unitOfWork.CommitTransaction();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await _unitOfWork.RollbackTransaction();
                    throw new Exception("Concurrency error: Another user has already reserved some of these seats.");
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                await _unitOfWork.DisposeTransactionAsync();
                throw ex;
            }
        }

        public async Task<bool> ConfirmAsync(ConfirmBookingDTO bookingDTO)
        {
            try
            {
                var userId = httpContextAccess.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var ticketsForPayment = await _unitOfWork.Repository<HeldTicket>().FindAsync(he => he.UserId == Convert.ToInt32(userId) && 
                        he.FilmId == bookingDTO.FilmId 
                );

                

                var seatIds = ticketsForPayment.Select(t=>t.SeatId).ToList();

                if (ticketsForPayment.Count() <= 0) {

                    throw new Exception("You need to Checkout First !");
                
                }

                var slotID = ticketsForPayment.Select(tk => tk.SlotId).FirstOrDefault();

                foreach (var ticket in ticketsForPayment) {

                    if (ticket.HoldExpiration < DateTime.UtcNow) {

                        throw new InvalidOperationException("Session Expired!");
                    
                    }
                
                }

                var film = await _unitOfWork.Repository<Film>().FindSingleAsync(film => film.FilmId == bookingDTO.FilmId);

                var amount = film.Amount!;

                decimal TotalAmount = amount * ticketsForPayment.Count();

                var bookEntity = new Booking
                {
                    BookingDate = DateTime.UtcNow,
                    SlotId = slotID,
                    TotalCost = TotalAmount,
                    UserId = Convert.ToInt32(userId),
                    BookingSeats = seatIds.Select(seatId => new BookingSeat
                    {
                        SeatId = seatId
                    }).ToList()
                };

                _unitOfWork.BeginTransaction();

                await _unitOfWork.Repository<Booking>().InsertAsync(bookEntity);
                var repo =  _unitOfWork.Repository<HeldTicket>();
                
                foreach (var item in ticketsForPayment)
                {
                    await repo.DeleteAsync(item.HeldTicketId);
                }
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                await _unitOfWork.DisposeTransactionAsync();
                throw ex;
            }

            
        }



        private async Task<List<Seat>> TotalSeats(BookingDTO booking)
        {
            var totalSeats = (await _unitOfWork.Repository<Slot>().GetAllAsync(slot =>
                            slot.Include(sl => sl.Seats)))
                        .Where(se => se.FilmId == booking.FilmId
                                     && se.SlotDate == booking.date
                                     && se.ShowCategory == booking.category)
                        .Select(se => se.Seats
                            .Where(s => booking.SeatNumbers.Contains(s.Number)
                                && s.IsReserved == false
                            )
                            .ToList())
                        .ToList().FirstOrDefault();

            return totalSeats;
        }

        private async Task<IEnumerable<HeldTicket>> HeldSeats(Seat seat, int Flimid)
        {
            var heltseats = await _unitOfWork.Repository<HeldTicket>().FindAsync(he => he.SeatId == seat.SeatId &&
                        he.HoldExpiration > DateTime.UtcNow &&
                        he.SlotId == seat.SlotId &&
                        he.FilmId == Flimid
                    );
            return heltseats;
        }

    }
}
