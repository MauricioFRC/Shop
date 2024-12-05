using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations.OrderDetail;

public class CreateOrderDetailValidation : AbstractValidator<CreateOrderDetailRequest>
{
    public CreateOrderDetailValidation()
    {
        RuleFor(x => x.Price)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.OrderId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}
