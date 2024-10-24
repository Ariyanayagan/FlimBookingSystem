using Flim.Domain.DTO;
using FluentValidation;

namespace Flim.API.Validators
{
    public class SeatValidators : AbstractValidator<SeatDto>
    {
        public SeatValidators() {

            RuleFor(seat => seat.Row).NotEmpty();
            RuleFor(seat => seat.seatCount).GreaterThan(0);
            RuleFor(seat => seat.filmId).GreaterThan(0);
            RuleFor(seat => seat.SlotDate).NotEmpty();
            RuleFor(seat => seat.category).IsInEnum().WithMessage("Slot only Have 0,1 and 2. 0 for Morning," +
                " 1 for Afternoon and 2 for Midnight");
        }
    }
}
