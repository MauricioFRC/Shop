using Core.Interfaces.Service;
using Core.Request;
using FluentValidation;
using Infrastructure.Constanst;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

public class TicketController : BaseApiController
{
    private readonly ITicketService _ticketService;
    private readonly IValidator<CreateTicketRequest> _createTicketValidator;

    public TicketController(
        ITicketService ticketService, 
        IValidator<CreateTicketRequest> createTicketValidator
        )
    {
        _ticketService = ticketService;
        _createTicketValidator = createTicketValidator;
    }

    [HttpPost("create-ticket")]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest createTicketRequest, CancellationToken cancellationToken)
    {
        var result = await _createTicketValidator.ValidateAsync(createTicketRequest, cancellationToken);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new { x.ErrorMessage, x.PropertyName }));

        return Ok(await _ticketService.CreateTicket(createTicketRequest, cancellationToken));
    }

    // [Authorize(Roles = "Admin")]
    [HttpGet("get-ticket-by-id/{ticketId}")]
    public async Task<IActionResult> GetTicketById([FromRoute] int ticketId, CancellationToken cancellationToken)
    {
        return Ok(await _ticketService.GetTicketById(ticketId, cancellationToken));
    }

    // [Authorize(Roles = "Admin")]
    [HttpGet("get-ticket-by-priority")]
    public async Task<IActionResult> GetTicketByPriority([FromQuery] EPriority priority, CancellationToken cancellationToken)
    {
        return Ok(await _ticketService.GetTicketByPriority(priority.ToString(), cancellationToken));
    }
}
