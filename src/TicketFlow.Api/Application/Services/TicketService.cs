using TicketFlow.Api.Api.Requests;
using TicketFlow.Api.Application.Common;
using TicketFlow.Api.Domain.Enums;
using TicketFlow.Api.Domain.Events;
using TicketFlow.Api.Domain.Interfaces;
using TicketFlow.Api.Domain.Models;

namespace TicketFlow.Api.Application.Services;

public sealed class TicketService
{
    private readonly IRepository<Ticket> _tickets;
    private readonly IRepository<Customer> _customers;
    private readonly IRepository<SupportAgent> _agents;

    public TicketService(
        IRepository<Ticket> tickets,
        IRepository<Customer> customers,
        IRepository<SupportAgent> agents
    )
    {
        _tickets = tickets;
        _customers = customers;
        _agents = agents;
    }

    public event TicketChangedHandler? TicketChanged;

    public async Task<OperationResult<Ticket>> CreateAsync(
        CreateTicketRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var customer = await _customers.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return OperationResult<Ticket>.Fail("Nie znaleziono klienta o podanym Id.");
        }

        Ticket ticket = request.Kind switch
        {
            TicketKind.Incident => new IncidentTicket(
                request.Title,
                request.Description,
                request.CustomerId,
                request.Priority
            ),
            TicketKind.ServiceRequest => new ServiceRequestTicket(
                request.Title,
                request.Description,
                request.CustomerId,
                request.Priority
            ),
            _ => throw new ArgumentOutOfRangeException(nameof(request.Kind))
        };

        await _tickets.AddAsync(ticket, cancellationToken);
        customer.AddTicket(ticket);
        await PublishTicketChangedAsync(ticket, "Utworzono nowe zgłoszenie.", cancellationToken);

        return OperationResult<Ticket>.Ok(ticket);
    }

    public Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _tickets.GetAllAsync(cancellationToken);
    }

    public Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _tickets.GetByIdAsync(id, cancellationToken);
    }

    public async Task<OperationResult<Ticket>> AssignAsync(
        Guid ticketId,
        Guid agentId,
        CancellationToken cancellationToken = default
    )
    {
        var ticket = await _tickets.GetByIdAsync(ticketId, cancellationToken);
        if (ticket is null)
        {
            return OperationResult<Ticket>.Fail("Nie znaleziono zgłoszenia.");
        }

        var agent = await _agents.GetByIdAsync(agentId, cancellationToken);
        if (agent is null)
        {
            return OperationResult<Ticket>.Fail("Nie znaleziono pracownika wsparcia.");
        }

        try
        {
            ticket.AssignTo(agent);
        }
        catch (InvalidOperationException exception)
        {
            return OperationResult<Ticket>.Fail(exception.Message);
        }

        await PublishTicketChangedAsync(ticket, $"Przypisano zgłoszenie do: {agent.FullName}.", cancellationToken);
        return OperationResult<Ticket>.Ok(ticket);
    }

    public async Task<OperationResult<Ticket>> ChangeStatusAsync(
        Guid ticketId,
        TicketStatus status,
        CancellationToken cancellationToken = default
    )
    {
        var ticket = await _tickets.GetByIdAsync(ticketId, cancellationToken);
        if (ticket is null)
        {
            return OperationResult<Ticket>.Fail("Nie znaleziono zgłoszenia.");
        }

        var previousStatus = ticket.Status;
        ticket.ChangeStatus(status);
        await PublishTicketChangedAsync(
            ticket,
            $"Zmieniono status z {previousStatus} na {status}.",
            cancellationToken
        );

        return OperationResult<Ticket>.Ok(ticket);
    }

    public async Task<OperationResult<Ticket>> AddCommentAsync(
        Guid ticketId,
        string comment,
        CancellationToken cancellationToken = default
    )
    {
        var ticket = await _tickets.GetByIdAsync(ticketId, cancellationToken);
        if (ticket is null)
        {
            return OperationResult<Ticket>.Fail("Nie znaleziono zgłoszenia.");
        }

        try
        {
            ticket.AddComment(comment);
        }
        catch (ArgumentException exception)
        {
            return OperationResult<Ticket>.Fail(exception.Message);
        }

        await PublishTicketChangedAsync(ticket, "Dodano komentarz do zgłoszenia.", cancellationToken);
        return OperationResult<Ticket>.Ok(ticket);
    }

    public async Task<IReadOnlyList<Ticket>> GetOverdueAsync(CancellationToken cancellationToken = default)
    {
        var tickets = await _tickets.GetAllAsync(cancellationToken);
        var now = DateTimeOffset.UtcNow;

        return tickets
            .Where(ticket => ticket.Status is not TicketStatus.Resolved and not TicketStatus.Closed)
            .Where(ticket => ticket.IsOverdue(now))
            .ToList();
    }

    private async Task PublishTicketChangedAsync(
        Ticket ticket,
        string description,
        CancellationToken cancellationToken
    )
    {
        var handlers = TicketChanged?.GetInvocationList();
        if (handlers is null)
        {
            return;
        }

        var eventArgs = new TicketChangedEventArgs(ticket, description, DateTimeOffset.UtcNow);

        foreach (TicketChangedHandler handler in handlers)
        {
            await handler(this, eventArgs, cancellationToken);
        }
    }
}
