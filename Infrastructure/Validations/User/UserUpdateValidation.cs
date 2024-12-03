using Core.DTOs.User;
using FluentValidation;

namespace Infrastructure.Validations.User;

public class UserUpdateValidation : AbstractValidator<UserUpdateDTO>
{
    public UserUpdateValidation()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3);

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6);
    }
}
