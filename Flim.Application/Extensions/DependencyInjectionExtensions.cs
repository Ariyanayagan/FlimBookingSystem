using Flim.Application.Interfaces;
using Flim.Application.Services;
using Flim.Domain.Shared;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IShowtimeService, ShowtimeService>();
            services.AddScoped<ISlotService, SlotService>();
            services.AddScoped<ISeatService, SeatService>();
            services.AddScoped<IBookingService, BookingService>();
            return services;
        }
    }
}
