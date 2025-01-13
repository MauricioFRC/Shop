using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations.Product;

public class CreateProductImgValidation : AbstractValidator<CreateProductRequestImg>
{
    private readonly List<string> ValidTypes = [".jpg", ".png", ".webp", ".jpeg"];
    private const int MaxFileSize = 5 * 1024 * 1024;

    public CreateProductImgValidation()
    {
        RuleFor(x => x.ProductImage)
            .Must(file => file.Length > 0)
            .Must(file => file.Length <= MaxFileSize).WithMessage("El tamaño máximo de las imagenes son 5MB.");

        RuleFor(x => x.ProductName)
            .NotEmpty().NotNull();

        RuleFor(x => x.ProductDescription)
            .NotEmpty().NotNull().MaximumLength(2048);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Stock)
            .GreaterThan(0);
    }

    private bool IsValidExtensions(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return false;

        var extensions = Path.GetExtension(fileName.ToLower());
        return ValidTypes.Contains(extensions);
    }
}
