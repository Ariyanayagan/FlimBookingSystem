using Flim.API.Model;
using FluentValidation;

namespace Flim.API.Validators
{
    public class LoginValidators : AbstractValidator<LoginModel>
    {
        public LoginValidators()
        {
            RuleFor(user => user.email).EmailAddress();
            RuleFor(user => user.password).NotEmpty();
        }
    }
}
