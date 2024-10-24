using Flim.API.Model;
using FluentValidation;

namespace Flim.API.Validators
{
    public class RegistorValidators : AbstractValidator<RegisterModel>
    {
        public RegistorValidators()
        {
            RuleFor(user => user.Email).EmailAddress();
            RuleFor(user => user.Username).NotEmpty();
            RuleFor(user => user.Password)
                        .NotEmpty().WithMessage("Password is required.")
                        .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                        .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                        .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                        .Matches(@"\d").WithMessage("Password must contain at least one number.")
                        .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");
        }
    }
}
