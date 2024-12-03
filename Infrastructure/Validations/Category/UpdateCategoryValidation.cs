using Core.DTOs.Category;
using FluentValidation;

namespace Infrastructure.Validations.Category;

public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryDTO>
{
    public UpdateCategoryValidation()
    {
        RuleFor(x => x.CategoryName)
            .NotEmpty()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(60);
    }
}
