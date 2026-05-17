using Microsoft.AspNetCore.Mvc;
using TicketFlow.Api.Application.Reports;

namespace TicketFlow.Api.Api.Controllers;

[ApiController]
[Route("api/reports")]
public sealed class ReportsController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportsController(ReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("tickets.{format}")]
    public async Task<IActionResult> ExportTickets(string format, CancellationToken cancellationToken)
    {
        var report = await _reportService.ExportAsync(format, cancellationToken);

        if (report is null)
        {
            return BadRequest(new { Error = "Dostępne formaty: txt, csv." });
        }

        var contentType = format.Equals("csv", StringComparison.OrdinalIgnoreCase)
            ? "text/csv"
            : "text/plain";

        return Content(report, contentType);
    }
}
