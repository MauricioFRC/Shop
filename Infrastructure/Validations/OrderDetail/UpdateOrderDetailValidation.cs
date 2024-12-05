using Core.DTOs.OrderDetail;
using FluentValidation;

namespace Infrastructure.Validations.OrderDetail;

public class UpdateOrderDetailValidation : AbstractValidator<UpdateOrderDetailDTO>
{
    public UpdateOrderDetailValidation()
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
