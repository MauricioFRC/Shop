using Core.DTOs.User;
using FluentValidation;

namespace Infrastructure.Validations.User;

public class UpdateUserRoleValidation : AbstractValidator<UpdateUserRoleDto>
{
    private readonly List<string> _roles = new List<string> { "Customer", "Admin" };

    public UpdateUserRoleValidation()
    {
        RuleFor(x => x.Role)
            .NotEmpty()
            .NotNull()
            .Must(role => _roles.Contains(role))
            .WithMessage($"Los roles permitidos son: {string.Join(", ", _roles)}");
    }
}
