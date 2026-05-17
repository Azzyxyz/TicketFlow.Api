using TicketFlow.Api.Domain.Attributes;
using TicketFlow.Api.Domain.Enums;

namespace TicketFlow.Api.Domain.Models;

[MetaDescription("Zgłoszenie prośby o usługę, np. utworzenie konta lub zmianę konfiguracji.")]
public sealed class ServiceRequestTicket : Ticket
{
    public ServiceRequestTicket(
        string title,
        string description,
        Guid customerId,
        TicketPriority priority,
        Guid? id = null
    ) : base(title, description, customerId, priority, id)
    {
    }

    public override TicketKind Kind => TicketKind.ServiceRequest;

    public override Money EstimateCost()
    {
        return SlaPolicy.GetBasePriceFor(Priority) * 0.9m;
    }
}
