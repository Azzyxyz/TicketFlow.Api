using TicketFlow.Api.Domain.Enums;

namespace TicketFlow.Api.Api.Requests;

public sealed class CreateTicketRequest
{
    public required string Title { get; init; }

    public required string Description { get; init; }

    public required Guid CustomerId { get; init; }

    public TicketPriority Priority { get; init; } = TicketPriority.Normal;

    public TicketKind Kind { get; init; } = TicketKind.Incident;
}
