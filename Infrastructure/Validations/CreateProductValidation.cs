using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations;

public class CreateProductValidation : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().NotNull();

        RuleFor(x => x.Description)
            .NotEmpty().NotNull().MaximumLength(120);

        RuleFor(x => x.Category)
            .MaximumLength(40);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Stock)
            .GreaterThan(0);
    }
}
