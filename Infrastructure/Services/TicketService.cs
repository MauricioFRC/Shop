using Core.DTOs.Ticket;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;

namespace Infrastructure.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _tickerRepository;

    public TicketService(ITicketRepository tickerRepository)
    {
        _tickerRepository = tickerRepository;
    }

    public async Task<TicketResponseDto> CreateTicket(CreateTicketRequest createTicketRequest, CancellationToken cancellationToken)
    {
        var createdTicket = await _tickerRepository.CreateTicket(createTicketRequest, cancellationToken)
            ?? throw new ArgumentNullException("No se pudo crear el ticket");

        return createdTicket;
    }

    public async Task<TicketResponseDto> GetTicketById(int id, CancellationToken cancellationToken)
    {
        ValidateId(id);
        var ticket = await _tickerRepository.GetTicketById(id, cancellationToken)
            ?? throw new ArgumentNullException($"No se encontró el ticket con el Id: {id}");

        return ticket;
    }

    public async Task<List<TicketResponseDto>> GetTicketByPriority(string priority, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(priority)) throw new ArgumentNullException($"No se encontró la prioridad {priority}");
        var ticket = await _tickerRepository.GetTicketByPriority(priority, cancellationToken);

        return ticket;
    }

    private static void ValidateId(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
    }
}
