using TicketFlow.Api.Domain.Attributes;

namespace TicketFlow.Api.Domain.Models;

[MetaDescription("Pracownik wsparcia, do którego można przypisać zgłoszenie.")]
public sealed class SupportAgent : Person
{
    public SupportAgent(
        string fullName,
        string email,
        string specialization,
        bool isAvailable = true,
        Guid? id = null
    ) : base(fullName, email, id)
    {
        Specialization = string.IsNullOrWhiteSpace(specialization)
            ? "Ogólne wsparcie"
            : specialization.Trim();
        IsAvailable = isAvailable;
    }

    [MetaDescription("Obszar, w którym agent najlepiej pomaga.")]
    public string Specialization { get; }

    [MetaDescription("Informacja, czy agent jest dostępny.")]
    public bool IsAvailable { get; private set; }

    public void ChangeAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }
}
