using TicketFlow.Api.Domain.Attributes;
using TicketFlow.Api.Domain.Common;

namespace TicketFlow.Api.Domain.Models;

[MetaDescription("Wspólna baza dla osób występujących w systemie.")]
public abstract class Person : Entity
{
    protected Person(string fullName, string email, Guid? id = null) : base(id)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Imię i nazwisko są wymagane.", nameof(fullName));
        }

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            throw new ArgumentException("Adres e-mail jest niepoprawny.", nameof(email));
        }

        FullName = fullName.Trim();
        Email = email.Trim().ToLowerInvariant();
    }

    [MetaDescription("Imię i nazwisko osoby.")]
    public string FullName { get; }

    [MetaDescription("Adres e-mail osoby.")]
    public string Email { get; }
}
