using Microsoft.AspNetCore.Mvc;
using TicketFlow.Api.Api.Requests;
using TicketFlow.Api.Domain.Interfaces;
using TicketFlow.Api.Domain.Models;

namespace TicketFlow.Api.Api.Controllers;

[ApiController]
[Route("api/customers")]
public sealed class CustomersController : ControllerBase
{
    private readonly IRepository<Customer> _customers;

    public CustomersController(IRepository<Customer> customers)
    {
        _customers = customers;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _customers.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _customers.GetByIdAsync(id, cancellationToken);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.FullName, request.Email, request.CompanyName);
        await _customers.AddAsync(customer, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }
}
