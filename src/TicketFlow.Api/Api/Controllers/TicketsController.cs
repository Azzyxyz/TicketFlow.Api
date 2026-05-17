using Microsoft.AspNetCore.Mvc;
using TicketFlow.Api.Api.Requests;
using TicketFlow.Api.Application.Services;

namespace TicketFlow.Api.Api.Controllers;

[ApiController]
[Route("api/tickets")]
public sealed class TicketsController : ControllerBase
{
    private readonly TicketService _ticketService;

    public TicketsController(TicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tickets = await _ticketService.GetAllAsync(cancellationToken);
        return Ok(tickets);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var ticket = await _ticketService.GetByIdAsync(id, cancellationToken);
        return ticket is null ? NotFound() : Ok(ticket);
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdue(CancellationToken cancellationToken)
    {
        var tickets = await _ticketService.GetOverdueAsync(cancellationToken);
        return Ok(tickets);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTicketRequest request, CancellationToken cancellationToken)
    {
        var result = await _ticketService.CreateAsync(request, cancellationToken);

        if (!result.Success || result.Value is null)
        {
            return BadRequest(new { result.Error });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    [HttpPut("{id:guid}/assign")]
    public async Task<IActionResult> Assign(Guid id, AssignTicketRequest request, CancellationToken cancellationToken)
    {
        var result = await _ticketService.AssignAsync(id, request.AgentId, cancellationToken);
        return result.Success ? Ok(result.Value) : BadRequest(new { result.Error });
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, UpdateTicketStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await _ticketService.ChangeStatusAsync(id, request.Status, cancellationToken);
        return result.Success ? Ok(result.Value) : BadRequest(new { result.Error });
    }

    [HttpPost("{id:guid}/comments")]
    public async Task<IActionResult> AddComment(Guid id, AddCommentRequest request, CancellationToken cancellationToken)
    {
        var result = await _ticketService.AddCommentAsync(id, request.Comment, cancellationToken);
        return result.Success ? Ok(result.Value) : BadRequest(new { result.Error });
    }
}
