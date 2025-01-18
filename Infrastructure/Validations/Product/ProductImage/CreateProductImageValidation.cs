using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations.Product.ProductImage;

public class CreateProductImageValidation : AbstractValidator<CreateProductImageRequest>
{
    public CreateProductImageValidation()
    {
        RuleFor(x => x.File)
            .NotEmpty()
            .NotNull();
    }
}
