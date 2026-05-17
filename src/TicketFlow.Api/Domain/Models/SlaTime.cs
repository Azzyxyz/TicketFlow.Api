using TicketFlow.Api.Domain.Attributes;

namespace TicketFlow.Api.Domain.Models;

[MetaDescription("Obiekt wartości reprezentujący limit czasu SLA.")]
public readonly struct SlaTime : IComparable<SlaTime>
{
    public SlaTime(TimeSpan duration)
    {
        if (duration < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(duration), "SLA nie może być ujemne.");
        }

        Duration = duration;
    }

    [MetaDescription("Czas trwania limitu SLA.")]
    public TimeSpan Duration { get; }

    public static SlaTime FromHours(double hours)
    {
        return new SlaTime(TimeSpan.FromHours(hours));
    }

    public int CompareTo(SlaTime other)
    {
        return Duration.CompareTo(other.Duration);
    }

    public static SlaTime operator +(SlaTime left, SlaTime right)
    {
        return new SlaTime(left.Duration + right.Duration);
    }

    public static bool operator >(SlaTime left, SlaTime right)
    {
        return left.Duration > right.Duration;
    }

    public static bool operator <(SlaTime left, SlaTime right)
    {
        return left.Duration < right.Duration;
    }

    public static bool operator >=(SlaTime left, SlaTime right)
    {
        return left.Duration >= right.Duration;
    }

    public static bool operator <=(SlaTime left, SlaTime right)
    {
        return left.Duration <= right.Duration;
    }

    public override string ToString()
    {
        return $"{Duration.TotalHours:0.#} h";
    }
}
