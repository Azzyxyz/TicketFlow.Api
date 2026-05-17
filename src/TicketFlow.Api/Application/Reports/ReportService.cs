using TicketFlow.Api.Domain.Interfaces;
using TicketFlow.Api.Domain.Models;

namespace TicketFlow.Api.Application.Reports;

public sealed class ReportService
{
    private readonly IEnumerable<IReportExporter> _exporters;
    private readonly IRepository<Ticket> _tickets;

    public ReportService(IEnumerable<IReportExporter> exporters, IRepository<Ticket> tickets)
    {
        _exporters = exporters;
        _tickets = tickets;
    }

    public async Task<string?> ExportAsync(string format, CancellationToken cancellationToken = default)
    {
        var exporter = _exporters.FirstOrDefault(item =>
            item.Format.Equals(format, StringComparison.OrdinalIgnoreCase)
        );

        if (exporter is null)
        {
            return null;
        }

        var tickets = await _tickets.GetAllAsync(cancellationToken);
        return await exporter.ExportAsync(tickets, cancellationToken);
    }
}
