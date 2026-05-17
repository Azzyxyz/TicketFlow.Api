using TicketFlow.Api.Domain.Attributes;

namespace TicketFlow.Api.Domain.Common;

[MetaDescription("Bazowa klasa każdej encji domenowej. Zapewnia Id oraz datę utworzenia.")]
public abstract class Entity
{
    protected Entity(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
    }

    [MetaDescription("Unikalny identyfikator obiektu.")]
    public Guid Id { get; }

    [MetaDescription("Data utworzenia obiektu w czasie UTC.")]
    public DateTimeOffset CreatedAt { get; }

    public override bool Equals(object? obj)
    {
        return obj is Entity entity && entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
