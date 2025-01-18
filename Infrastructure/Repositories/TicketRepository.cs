using Core.DTOs.Ticket;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Request;
using Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly AplicationDbContext _context;

    public TicketRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TicketResponseDto> CreateTicket(CreateTicketRequest createTicketRequest, CancellationToken cancellationToken)
    {
        var createdTicket = createTicketRequest.Adapt<Ticket>();

        await _context.Tickets.AddAsync(createdTicket, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var getCreatedTicket = await _context.Tickets
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == createdTicket.Id, cancellationToken);

        return getCreatedTicket.Adapt<TicketResponseDto>();
    }

    public async Task<TicketResponseDto> GetTicketById(int id, CancellationToken cancellationToken)
    {
        var getTicket = await _context.Tickets
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return getTicket.Adapt<TicketResponseDto>();
    }

    public async Task<List<TicketResponseDto>> GetTicketByPriority(string priority, CancellationToken cancellationToken)
    {
        var getTicketByPriority = await _context.Tickets
            .Include(x => x.User)
            .Where(x => x.Priority == priority)
            .ToListAsync(cancellationToken);

        return getTicketByPriority.Adapt<List<TicketResponseDto>>();
    }
}
