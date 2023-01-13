using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ResultVideo.Api.Attributes;
using ResultVideo.Api.Contracts.Requests;
using ResultVideo.Api.Mapping;
using ResultVideo.Api.Services;
using ResultVideo.Api.Validation;

namespace ResultVideo.Api.Controllers;

[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost("customers")]
    public async Task<IActionResult> Create([FromBody] CustomerRequest request)
    {
        var customer = request.ToCustomer();

        var result = await _customerService.CreateAsync(customer);

        return result.Match<IActionResult>(cust =>
        {
            var customerResponse = cust.ToCustomerResponse();
            return CreatedAtAction("Get", new { customerResponse.Id }, customerResponse);
        }, exception =>
        {
            if (exception is ValidationException validationException)
            {
                return BadRequest(validationException.ToProblemDetails());
            }

            return StatusCode(500);
        });
    }

    [HttpGet("customers/{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var customer = await _customerService.GetAsync(id);

        if (customer is null)
        {
            return NotFound();
        }

        var customerResponse = customer.ToCustomerResponse();
        return Ok(customerResponse);
    }
    
    [HttpGet("customers")]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        var customersResponse = customers.ToCustomersResponse();
        return Ok(customersResponse);
    }
    
    [HttpPut("customers/{id:guid}")]
    public async Task<IActionResult> Update(
        [FromMultiSource] UpdateCustomerRequest request)
    {
        var existingCustomer = await _customerService.GetAsync(request.Id);

        if (existingCustomer is null)
        {
            return NotFound();
        }

        var customer = request.ToCustomer();
        var result = await _customerService.UpdateAsync(customer);
        return result.ToOk(c => c.ToCustomerResponse());
    }
    
    [HttpDelete("customers/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _customerService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}
