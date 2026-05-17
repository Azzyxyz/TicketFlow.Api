using TicketFlow.Api.Domain.Events;
using TicketFlow.Api.Domain.Interfaces;

namespace TicketFlow.Api.Application.Notifications;

public sealed class NotificationService
{
    private readonly IEnumerable<INotificationChannel> _channels;

    public NotificationService(IEnumerable<INotificationChannel> channels)
    {
        _channels = channels;
    }

    public async Task HandleTicketChangedAsync(
        object sender,
        TicketChangedEventArgs eventArgs,
        CancellationToken cancellationToken = default
    )
    {
        foreach (var channel in _channels)
        {
            await channel.SendAsync(eventArgs, cancellationToken);
        }
    }
}
