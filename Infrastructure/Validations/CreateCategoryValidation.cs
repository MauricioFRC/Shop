using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations;

public class CreateCategoryValidation : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidation()
    {
        RuleFor(x => x.CategoryName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(60)
            .MinimumLength(3);
    }
}
