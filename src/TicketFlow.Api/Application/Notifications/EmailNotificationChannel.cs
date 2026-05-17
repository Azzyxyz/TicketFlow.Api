using TicketFlow.Api.Domain.Events;
using TicketFlow.Api.Domain.Interfaces;

namespace TicketFlow.Api.Application.Notifications;

public sealed class EmailNotificationChannel : INotificationChannel
{
    public string Name => "email";

    public async Task SendAsync(TicketChangedEventArgs eventArgs, CancellationToken cancellationToken = default)
    {
        await Task.Delay(50, cancellationToken);
        Console.WriteLine($"[{Name}] Wysłano powiadomienie o zgłoszeniu {eventArgs.Ticket.Id}.");
    }
}
