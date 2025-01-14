using Core.Request;
using FluentValidation;
using Microsoft.AspNetCore.Http;

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
