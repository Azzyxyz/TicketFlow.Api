namespace TicketFlow.Api.Api.Requests;

public sealed class CreateCustomerRequest
{
    public required string FullName { get; init; }

    public required string Email { get; init; }

    public required string CompanyName { get; init; }
}
