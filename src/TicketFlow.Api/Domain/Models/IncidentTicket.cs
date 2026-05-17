using TicketFlow.Api.Domain.Attributes;
using TicketFlow.Api.Domain.Enums;

namespace TicketFlow.Api.Domain.Models;

[MetaDescription("Zgłoszenie awarii, czyli problemu wymagającego naprawy.")]
public sealed class IncidentTicket : Ticket
{
    public IncidentTicket(
        string title,
        string description,
        Guid customerId,
        TicketPriority priority,
        Guid? id = null
    ) : base(title, description, customerId, priority, id)
    {
    }

    public override TicketKind Kind => TicketKind.Incident;

    public override Money EstimateCost()
    {
        return SlaPolicy.GetBasePriceFor(Priority) * 1.35m;
    }
}
