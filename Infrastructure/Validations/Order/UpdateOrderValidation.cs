using Core.DTOs.Order;
using FluentValidation;

namespace Infrastructure.Validations.Order;

public class UpdateOrderValidation : AbstractValidator<UpdateOrderDTO>
{
    public UpdateOrderValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.OrderDate)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Status)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3);

        RuleFor(x => x.TotalAmount)
            .NotEmpty()
            .GreaterThan(0);
    }
}
