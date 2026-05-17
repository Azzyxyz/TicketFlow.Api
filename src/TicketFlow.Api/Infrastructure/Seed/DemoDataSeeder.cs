using TicketFlow.Api.Domain.Enums;
using TicketFlow.Api.Domain.Interfaces;
using TicketFlow.Api.Domain.Models;

namespace TicketFlow.Api.Infrastructure.Seed;

public static class DemoDataSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var customers = scope.ServiceProvider.GetRequiredService<IRepository<Customer>>();
        var agents = scope.ServiceProvider.GetRequiredService<IRepository<SupportAgent>>();
        var tickets = scope.ServiceProvider.GetRequiredService<IRepository<Ticket>>();

        var customer = new Customer(
            "Anna Kowalska",
            "anna.kowalska@example.com",
            "Kowalska Studio",
            Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
        );

        var agent = new SupportAgent(
            "Piotr Nowak",
            "piotr.nowak@example.com",
            "Aplikacje webowe",
            true,
            Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")
        );

        var incident = new IncidentTicket(
            "Nie działa logowanie",
            "Klient nie może zalogować się po zmianie hasła.",
            customer.Id,
            TicketPriority.High,
            Guid.Parse("11111111-1111-1111-1111-111111111111")
        );
        incident.AssignTo(agent);
        incident.AddComment("Sprawdzono historię logowania i blokadę konta.");

        var request = new ServiceRequestTicket(
            "Dodanie nowego użytkownika",
            "Prośba o utworzenie konta dla nowego pracownika.",
            customer.Id,
            TicketPriority.Normal,
            Guid.Parse("22222222-2222-2222-2222-222222222222")
        );

        await customers.AddAsync(customer);
        await agents.AddAsync(agent);
        await tickets.AddAsync(incident);
        await tickets.AddAsync(request);

        customer.AddTicket(incident);
        customer.AddTicket(request);
    }
}
