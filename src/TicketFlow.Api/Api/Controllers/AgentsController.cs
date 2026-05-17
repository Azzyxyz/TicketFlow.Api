using Microsoft.AspNetCore.Mvc;
using TicketFlow.Api.Api.Requests;
using TicketFlow.Api.Domain.Interfaces;
using TicketFlow.Api.Domain.Models;

namespace TicketFlow.Api.Api.Controllers;

[ApiController]
[Route("api/agents")]
public sealed class AgentsController : ControllerBase
{
    private readonly IRepository<SupportAgent> _agents;

    public AgentsController(IRepository<SupportAgent> agents)
    {
        _agents = agents;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _agents.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var agent = await _agents.GetByIdAsync(id, cancellationToken);
        return agent is null ? NotFound() : Ok(agent);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSupportAgentRequest request, CancellationToken cancellationToken)
    {
        var agent = new SupportAgent(request.FullName, request.Email, request.Specialization, request.IsAvailable);
        await _agents.AddAsync(agent, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = agent.Id }, agent);
    }
}
