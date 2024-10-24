using Flim.Application.DTOs;
using FluentValidation;

namespace Flim.API.Validators
{
    public class ConfirmBookingValidators : AbstractValidator<ConfirmBookingDTO>
    {
        public ConfirmBookingValidators()
        {
            RuleFor(cf => cf.FilmId).NotEmpty().GreaterThan(0);
            //RuleFor(cf => cf.category).NotEmpty().IsInEnum();
        }
    }
}
