using Core.DTOs.Ticket;
using Core.Entities;
using Core.Request;

namespace Core.Interfaces.Repository;

public interface ITicketRepository
{
    #region CRUD
    public Task<TicketResponseDto> CreateTicket(CreateTicketRequest createTicketRequest, CancellationToken cancellationToken);
    public Task<TicketResponseDto> GetTicketById(int id, CancellationToken cancellationToken);
    public Task<List<TicketResponseDto>> GetTicketByPriority(string priority, CancellationToken cancellationToken);
    #endregion

    #region Validate
    public Task<User> CheckIfExist(int id);
    #endregion
}
