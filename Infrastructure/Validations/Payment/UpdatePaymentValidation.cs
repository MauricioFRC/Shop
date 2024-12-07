using Core.DTOs.Payment;
using FluentValidation;

namespace Infrastructure.Validations.Payment;

public class UpdatePaymentValidation : AbstractValidator<UpdatePaymentDTO>
{
    public UpdatePaymentValidation()
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.PaymentMethod)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3);

        RuleFor(x => x.OrderId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        var minDate = new DateTime(2020, 1, 1);
        RuleFor(x => x.PaymentDate)
            .NotEmpty()
            .NotNull()
            .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de pago no puede ser en el futuro.")
            .GreaterThanOrEqualTo(minDate).WithMessage($"La fecha de pago debe ser posterior a {minDate:dd/MM/yyyy}.");
    }
}
