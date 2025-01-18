using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations.Ticket;

public class CreateTicketValidation : AbstractValidator<CreateTicketRequest>
{
    private readonly List<string> ValidStatuses = new() { "En revision", "Cerrado", "Pendiente" };
    private readonly List<string> Priorities = new() { "Baja", "Media", "Alta" };
    private readonly DateTime ActualDate = DateTime.UtcNow;

    public CreateTicketValidation()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0).WithMessage("El id no puede se negativo ni cero");

        RuleFor(x => x.Subject)
            .NotNull()
            .NotEmpty()
            .MinimumLength(20)
            .MaximumLength(100)
            .WithMessage("La descripción debe tener por lo menos 20 caracteres y como maximo 100.");

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .MinimumLength(20)
            .MaximumLength(500)
            .WithMessage("La descripción debe tener por lo menos 20 caracteres y como maximo 500.");

        RuleFor(x => x.Priority)
            .NotNull()
            .NotEmpty()
            .Must(priority => Priorities.Contains(priority))
            .WithMessage($"Las prioridades validas son: {string.Join(", ", Priorities)}");

        RuleFor(x => x.Status)
            .NotEmpty()
            .NotNull()
            .Must(status => ValidStatuses.Contains(status))
            .WithMessage($"Los estados validos son: {string.Join(", ", ValidStatuses)}");

        RuleFor(x => x.CreateAt)
            .NotNull()
            .NotEmpty()
            .Must(date => date <= ActualDate)
            .WithMessage($"La creación del ticket no puede ser una fecha posterior a la actual.");
    }
}
