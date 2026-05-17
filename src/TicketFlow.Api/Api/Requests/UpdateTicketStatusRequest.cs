using TicketFlow.Api.Domain.Enums;

namespace TicketFlow.Api.Api.Requests;

public sealed class UpdateTicketStatusRequest
{
    public TicketStatus Status { get; init; }
}
