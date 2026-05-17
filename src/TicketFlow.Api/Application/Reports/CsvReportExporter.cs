using System.Text;
using TicketFlow.Api.Domain.Interfaces;
using TicketFlow.Api.Domain.Models;

namespace TicketFlow.Api.Application.Reports;

public sealed class CsvReportExporter : IReportExporter
{
    public string Format => "csv";

    public Task<string> ExportAsync(IEnumerable<Ticket> tickets, CancellationToken cancellationToken = default)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Id,Kind,Priority,Status,Title,EstimatedCost");

        foreach (var ticket in tickets)
        {
            var safeTitle = ticket.Title.Replace(',', ' ');
            builder.AppendLine($"{ticket.Id},{ticket.Kind},{ticket.Priority},{ticket.Status},{safeTitle},{ticket.EstimateCost().Amount:0.00}");
        }

        return Task.FromResult(builder.ToString());
    }
}
