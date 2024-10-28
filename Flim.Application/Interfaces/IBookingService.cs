using Flim.Application.DTOs;
using Flim.Application.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Interfaces
{
    /// <summary>
    /// Contract of Booking service
    /// </summary>
    public interface IBookingService
    {
        Task<List<FilmRecord>> GetAvailableSeats(int id , DateOnly date);

        Task HoldAsync(BookingDTO booking);

        Task<bool> ConfirmAsync(ConfirmBookingDTO booking);


    }
}
