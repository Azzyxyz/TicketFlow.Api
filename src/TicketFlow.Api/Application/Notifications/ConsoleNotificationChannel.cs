using TicketFlow.Api.Domain.Events;
using TicketFlow.Api.Domain.Interfaces;

namespace TicketFlow.Api.Application.Notifications;

public sealed class ConsoleNotificationChannel : INotificationChannel
{
    public string Name => "console";

    public Task SendAsync(TicketChangedEventArgs eventArgs, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"[{Name}] Ticket {eventArgs.Ticket.Id}: {eventArgs.ChangeDescription}");
        return Task.CompletedTask;
    }
}
