using TicketFlow.Api.Domain.Enums;

namespace TicketFlow.Api.Domain.Models;

public static class SlaPolicy
{
    public static SlaTime GetLimitFor(TicketPriority priority)
    {
        return priority switch
        {
            TicketPriority.Critical => SlaTime.FromHours(2),
            TicketPriority.High => SlaTime.FromHours(8),
            TicketPriority.Normal => SlaTime.FromHours(24),
            TicketPriority.Low => SlaTime.FromHours(72),
            _ => SlaTime.FromHours(24)
        };
    }

    public static Money GetBasePriceFor(TicketPriority priority)
    {
        return priority switch
        {
            TicketPriority.Critical => new Money(600),
            TicketPriority.High => new Money(350),
            TicketPriority.Normal => new Money(200),
            TicketPriority.Low => new Money(120),
            _ => new Money(200)
        };
    }
}
