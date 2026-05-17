using TicketFlow.Api.Domain.Models;

namespace TicketFlow.Api.Domain.Events;

public sealed class TicketChangedEventArgs : EventArgs
{
    public TicketChangedEventArgs(Ticket ticket, string changeDescription, DateTimeOffset occurredAt)
    {
        Ticket = ticket;
        ChangeDescription = changeDescription;
        OccurredAt = occurredAt;
    }

    public Ticket Ticket { get; }

    public string ChangeDescription { get; }

    public DateTimeOffset OccurredAt { get; }
}
