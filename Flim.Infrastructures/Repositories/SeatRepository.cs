using Flim.Domain.DTO;
using Flim.Domain.Entities;
using Flim.Domain.Interfaces;
using Flim.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;


namespace Flim.Infrastructures.Repositories
{
    public class SeatRepository : GenericRepository<Seat>, ISeatRepository
    {
        public SeatRepository( BookingDbContext context) : base(context)
        {
        }

        public async Task AddSeat(SeatDto seatDto)
        {
            var film = await _context.Films
                .Include(f=>f.Slots)
                .FirstOrDefaultAsync(f => f.FilmId == seatDto.filmId);

            if (film == null)
            {
                throw new Exception("Film not found");
            }

            var filmSlots = film.Slots?.Where(  sl=>sl.SlotDate == seatDto.SlotDate && 
                                                sl.ShowCategory == seatDto.category
                                              ).ToList(); 

            if (!filmSlots.Any())
            {
                throw new Exception("No showtimes found for this film");
            }

            var seatsToInsert = new List<Seat>();

            foreach (var slot in filmSlots)
            {
                var existingHighestSeatNumber = await _context.Seats
                    .Where(s => s.SlotId == slot.SlotId)
                    .OrderBy(comparer => comparer.SlotId)
                    .Select(s => s.Number).ToListAsync();

                var num = (existingHighestSeatNumber.Count() == 0) ? 0 : existingHighestSeatNumber.Max();
                for (int i = 1; i <= seatDto.seatCount; i++)
                {
                    seatsToInsert.Add(new Seat
                    {
                        Number =  num + i,
                        Row = seatDto.Row,
                        IsReserved = false,
                        SlotId = slot.SlotId,
                    });
                }
            }

            await _context.Seats.AddRangeAsync(seatsToInsert);

        }


    }
}
