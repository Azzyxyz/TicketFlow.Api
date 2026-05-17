using TicketFlow.Api.Domain.Attributes;
using TicketFlow.Api.Domain.Common;
using TicketFlow.Api.Domain.Enums;

namespace TicketFlow.Api.Domain.Models;

[MetaDescription("Bazowa klasa zgłoszenia. Konkretne typy zgłoszeń dziedziczą po niej.")]
public abstract class Ticket : Entity
{
    private readonly List<string> _comments = new();

    protected Ticket(
        string title,
        string description,
        Guid customerId,
        TicketPriority priority,
        Guid? id = null
    ) : base(id)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Tytuł zgłoszenia jest wymagany.", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Opis zgłoszenia jest wymagany.", nameof(description));
        }

        Title = title.Trim();
        Description = description.Trim();
        CustomerId = customerId;
        Priority = priority;
        Status = TicketStatus.New;
        SlaLimit = SlaPolicy.GetLimitFor(priority);
    }

    [MetaDescription("Krótki tytuł problemu lub prośby.")]
    public string Title { get; }

    [MetaDescription("Dokładniejszy opis zgłoszenia.")]
    public string Description { get; }

    [MetaDescription("Id klienta, który utworzył zgłoszenie.")]
    public Guid CustomerId { get; }

    [MetaDescription("Id pracownika przypisanego do zgłoszenia.")]
    public Guid? AssignedAgentId { get; private set; }

    [MetaDescription("Aktualny status zgłoszenia.")]
    public TicketStatus Status { get; private set; }

    [MetaDescription("Priorytet zgłoszenia.")]
    public TicketPriority Priority { get; }

    [MetaDescription("Limit SLA zależny od priorytetu.")]
    public SlaTime SlaLimit { get; }

    [MetaDescription("Lista notatek dodanych do zgłoszenia.")]
    public IReadOnlyList<string> Comments => _comments;

    [MetaDescription("Typ konkretnego zgłoszenia.")]
    public abstract TicketKind Kind { get; }

    public string this[int index] => _comments[index];

    public void AssignTo(SupportAgent agent)
    {
        if (!agent.IsAvailable)
        {
            throw new InvalidOperationException("Nie można przypisać zgłoszenia do niedostępnego agenta.");
        }

        AssignedAgentId = agent.Id;
        Status = TicketStatus.Open;
    }

    public void ChangeStatus(TicketStatus status)
    {
        Status = status;
    }

    public void AddComment(string comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new ArgumentException("Komentarz nie może być pusty.", nameof(comment));
        }

        _comments.Add(comment.Trim());
    }

    public bool IsOverdue(DateTimeOffset now)
    {
        var age = new SlaTime(now - CreatedAt);
        return age > SlaLimit;
    }

    public abstract Money EstimateCost();
}
