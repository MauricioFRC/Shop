using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations.User;

public class CreateUserValidation : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidation()
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
