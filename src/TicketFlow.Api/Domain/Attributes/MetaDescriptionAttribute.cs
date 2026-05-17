namespace TicketFlow.Api.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public sealed class MetaDescriptionAttribute : Attribute
{
    public MetaDescriptionAttribute(string description)
    {
        Description = description;
    }

    public string Description { get; }
}
