using Microsoft.AspNetCore.Mvc;
using TicketFlow.Api.Application.Services;

namespace TicketFlow.Api.Api.Controllers;

[ApiController]
[Route("api/metadata")]
public sealed class MetadataController : ControllerBase
{
    private readonly ReflectionMetadataService _metadataService;

    public MetadataController(ReflectionMetadataService metadataService)
    {
        _metadataService = metadataService;
    }

    [HttpGet("domain")]
    public IActionResult GetDomainMetadata()
    {
        return Ok(_metadataService.DescribeDomain());
    }
}
