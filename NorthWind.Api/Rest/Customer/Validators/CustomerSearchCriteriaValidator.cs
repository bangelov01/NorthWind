using FluentValidation;
using NorthWind.Api.Rest.Customer.Models;

namespace NorthWind.Api.Rest.Customer.Validators;

internal sealed class CustomerSearchCriteriaValidator : AbstractValidator<CustomerSearchCriteria>
{
    public CustomerSearchCriteriaValidator()
    {
        RuleFor(criteria => criteria.CompanyName)
            .MaximumLength(30).WithMessage("Company name cannot be longer than 30 characters.")
            .When(criteria => !string.IsNullOrWhiteSpace(criteria.CompanyName));
    }
}
