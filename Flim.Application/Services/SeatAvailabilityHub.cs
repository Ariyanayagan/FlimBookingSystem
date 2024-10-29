using Flim.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Flim.Application.Services
{
    public class SeatAvailabilityHub : Hub
    {
        public async Task UpdateSeatAvailability(string seatIds, string isReserved)
        {
            await Clients.All.SendAsync("ReceiveSeatUpdate", seatIds, isReserved);
        }
    }
}
