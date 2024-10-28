using Flim.Application.DTOs;
using FluentValidation;

namespace Flim.API.Validators
{
    /// <summary>
    /// Validation for Booking ticakets using fluet API Validation
    /// </summary>
    public class BookingValidators:AbstractValidator<BookingDTO>
    {
        public BookingValidators()
        {
            RuleFor(booking => booking.FilmId).GreaterThan(0).WithMessage("Film Id Should be greater than zero!");

            RuleFor(booking => booking.category).IsInEnum().WithMessage("Slot only Have 0,1 and 2. 0 for Morning," +
                " 1 for Afternoon and 2 for Midnight");

            RuleFor(booking => booking.date).NotEmpty().WithMessage("Select a date!");
                  

            RuleFor(booking => booking.SeatNumbers).NotEmpty();
        }
    }
}
