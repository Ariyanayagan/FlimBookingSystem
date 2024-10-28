using Flim.Application.DTOs;
using FluentValidation;

namespace Flim.API.Validators
{
    /// <summary>
    /// Validation for Add film using fluent Validation
    /// </summary>
    public class AddFilmValidators : AbstractValidator<AddFilmDTO>
    {
        public AddFilmValidators()
        {
            RuleFor(addfilm => addfilm.Name).NotEmpty();
            RuleFor(addfilm => addfilm.Description).NotEmpty();
            RuleFor(addfilm => addfilm.Duration).NotEmpty().GreaterThan(0);
            RuleFor(addfilm => addfilm.Genre).NotEmpty();
            RuleFor(addfilm => addfilm.Amount).NotEmpty().GreaterThan(0);
        }
    }
}
