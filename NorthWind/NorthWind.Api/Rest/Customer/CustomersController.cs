using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Api.Rest.Customer.Models;
using NorthWind.Services.Customer;
using NorthWind.Services.Customer.Dto;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace NorthWind.Api.Rest.Customer;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<CustomerOverviewDto>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyCollection<CustomerOverviewDto>>> GetCustomers([FromQuery]CustomerSearchCriteria criteria, IValidator<CustomerSearchCriteria> validator)
    {
        ValidationResult? validationResult = await validator.ValidateAsync(criteria);

        if (!validationResult.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
        }

        IReadOnlyCollection<CustomerOverviewDto> customers = await customerService.GetCustomers(criteria.CompanyName);

        return Ok(customers);
    }

    [HttpGet("{id:length(5)}")]
    [ProducesResponseType(typeof(CustomerDetailsDto), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDetailsDto>> GetCustomerDetails([Required]string id)
    {
        CustomerDetailsDto? customer = await customerService.GetCustomerDetails(id);

        return customer is null ? NotFound() : customer;
    }
}
