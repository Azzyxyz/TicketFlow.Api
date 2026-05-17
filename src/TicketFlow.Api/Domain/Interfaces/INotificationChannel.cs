using TicketFlow.Api.Domain.Events;

namespace TicketFlow.Api.Domain.Interfaces;

public interface INotificationChannel
{
    string Name { get; }

    Task SendAsync(TicketChangedEventArgs eventArgs, CancellationToken cancellationToken = default);
}
