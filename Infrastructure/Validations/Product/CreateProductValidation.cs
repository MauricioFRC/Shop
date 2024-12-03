using Core.Request;
using FluentValidation;
using Infrastructure.Repositories;

namespace Infrastructure.Validations.Product;

public class CreateProductValidation : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().NotNull();

        RuleFor(x => x.Description)
            .NotEmpty().NotNull().MaximumLength(2048);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Stock)
            .GreaterThan(0);
    }
}
