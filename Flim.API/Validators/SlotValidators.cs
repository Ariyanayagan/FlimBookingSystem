using Flim.Application.DTOs;
using FluentValidation;

namespace Flim.API.Validators
{
    public class SlotValidators : AbstractValidator<SlotDTO>
    {
        public SlotValidators()
        {
            RuleFor(slot => slot.FilmId).GreaterThan(0);
            RuleFor(slot => slot.ShowCategory).IsInEnum().WithMessage("Slot only Have 0,1 and 2. 0 for Morning," +
                " 1 for Afternoon and 2 for Midnight");
            RuleFor(slot => slot.SlotDate).GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("The date cannot be in the past.");
        }
    }
}
