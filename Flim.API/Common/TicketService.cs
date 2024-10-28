
using Flim.Domain.Entities;
using Flim.Domain.Shared;

namespace Flim.API.Common
{
    /// <summary>
    /// A backround service which clean Hold tikets after 10 mins.
    /// this service run each minute.
    /// </summary>
    public class TicketService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

        public TicketService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_checkInterval, stoppingToken);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var heldTickets = await unitOfWork.Repository<HeldTicket>().FindAsync(ht => ht.HoldExpiration < DateTime.UtcNow);

                    foreach (var heldTicket in heldTickets)
                    {
                        // Release the held ticket and update seat status
                        var seat = await unitOfWork.SeatRepository.GetByIdAsync(heldTicket.SeatId);
                        if (seat != null)
                        {
                            seat.IsReserved = false; // Release hold
                            await unitOfWork.SeatRepository.UpdateAsync(seat);
                        }

                        await unitOfWork.Repository<HeldTicket>().DeleteAsync(heldTicket.HeldTicketId); // Remove held ticket
                    }
                    await unitOfWork.SaveAsync();
                }
            }
        }
    }
}
