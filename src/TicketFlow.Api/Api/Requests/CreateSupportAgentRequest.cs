namespace TicketFlow.Api.Api.Requests;

public sealed class CreateSupportAgentRequest
{
    public required string FullName { get; init; }

    public required string Email { get; init; }

    public required string Specialization { get; init; }

    public bool IsAvailable { get; init; } = true;
}
