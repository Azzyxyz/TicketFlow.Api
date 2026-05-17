using TicketFlow.Api.Domain.Attributes;

namespace TicketFlow.Api.Domain.Models;

[MetaDescription("Klient zakładający zgłoszenia w systemie helpdesk.")]
public sealed class Customer : Person
{
    private readonly List<Ticket> _tickets = new();

    public Customer(string fullName, string email, string companyName, Guid? id = null)
        : base(fullName, email, id)
    {
        CompanyName = string.IsNullOrWhiteSpace(companyName)
            ? "Klient indywidualny"
            : companyName.Trim();
    }

    [MetaDescription("Nazwa firmy klienta.")]
    public string CompanyName { get; }

    [MetaDescription("Zgłoszenia utworzone przez klienta.")]
    public IReadOnlyList<Ticket> Tickets => _tickets;

    public Ticket this[int index] => _tickets[index];

    public void AddTicket(Ticket ticket)
    {
        _tickets.Add(ticket);
    }
}
