using Core.DTOs.Ticket;
using Core.Request;

namespace Core.Interfaces.Repository;

public interface ITicketRepository
{
    public Task<TicketResponseDto> CreateTicket(CreateTicketRequest createTicketRequest, CancellationToken cancellationToken);
    public Task<TicketResponseDto> GetTicketById(int id, CancellationToken cancellationToken);
    public Task<List<TicketResponseDto>> GetTicketByPriority(string priority, CancellationToken cancellationToken);
}
