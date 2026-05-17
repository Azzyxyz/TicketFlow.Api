using TicketFlow.Api.Domain.Models;

namespace TicketFlow.Api.Domain.Interfaces;

public interface IReportExporter
{
    string Format { get; }

    Task<string> ExportAsync(IEnumerable<Ticket> tickets, CancellationToken cancellationToken = default);
}
