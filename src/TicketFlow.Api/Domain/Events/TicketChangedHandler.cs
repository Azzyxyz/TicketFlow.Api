namespace TicketFlow.Api.Domain.Events;

public delegate Task TicketChangedHandler(
    object sender,
    TicketChangedEventArgs eventArgs,
    CancellationToken cancellationToken = default
);
