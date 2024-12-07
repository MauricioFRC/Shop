using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations.Payment;

public class CreatePaymentValidation : AbstractValidator<CreatePaymentRequest>
{
    public CreatePaymentValidation()
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
            .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("La fecha de pago no puede ser en el futuro.")
            .GreaterThanOrEqualTo(minDate)
                .WithMessage($"La fecha de pago debe ser posterior a {minDate:dd/MM/yyyy}.");
    }
}
