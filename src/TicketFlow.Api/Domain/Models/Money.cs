using TicketFlow.Api.Domain.Attributes;

namespace TicketFlow.Api.Domain.Models;

[MetaDescription("Obiekt wartości przechowujący kwotę i walutę.")]
public readonly struct Money
{
    public Money(decimal amount, string currency = "PLN")
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Kwota nie może być ujemna.");
        }

        Amount = amount;
        Currency = string.IsNullOrWhiteSpace(currency) ? "PLN" : currency.ToUpperInvariant();
    }

    [MetaDescription("Wartość pieniężna.")]
    public decimal Amount { get; }

    [MetaDescription("Kod waluty, np. PLN.")]
    public string Currency { get; }

    public static Money Zero(string currency = "PLN")
    {
        return new Money(0, currency);
    }

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException("Nie można dodawać kwot w różnych walutach.");
        }

        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator *(Money money, decimal multiplier)
    {
        if (multiplier < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(multiplier), "Mnożnik nie może być ujemny.");
        }

        return new Money(money.Amount * multiplier, money.Currency);
    }

    public static bool operator >(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount > right.Amount;
    }

    public static bool operator <(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Amount < right.Amount;
    }

    private static void EnsureSameCurrency(Money left, Money right)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException("Nie można porównywać kwot w różnych walutach.");
        }
    }

    public override string ToString()
    {
        return $"{Amount:0.00} {Currency}";
    }
}
