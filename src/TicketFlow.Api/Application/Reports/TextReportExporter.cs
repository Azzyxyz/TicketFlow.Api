using System.Text;
using TicketFlow.Api.Domain.Interfaces;
using TicketFlow.Api.Domain.Models;

namespace TicketFlow.Api.Application.Reports;

public sealed class TextReportExporter : IReportExporter
{
    public string Format => "txt";

    public Task<string> ExportAsync(IEnumerable<Ticket> tickets, CancellationToken cancellationToken = default)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Raport TicketFlow");
        builder.AppendLine("=================");

        foreach (var ticket in tickets)
        {
            builder.AppendLine($"{ticket.Id} | {ticket.Kind} | {ticket.Priority} | {ticket.Status} | {ticket.Title} | {ticket.EstimateCost()}");
        }

        return Task.FromResult(builder.ToString());
    }
}
