using Core.DTOs.Product;
using FluentValidation;

namespace Infrastructure.Validations.Product;

public class UpdateProductValidation : AbstractValidator<UpdateProductDTO>
{
    public UpdateProductValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.Stock).GreaterThan(0);
    }
}
