using FluentValidation;
using NorthWind.Api.Rest.Customer.Models;

namespace NorthWind.Api.Rest.Customer.Validators;

internal sealed class CustomerSearchCriteriaValidator : AbstractValidator<CustomerSearchCriteria>
{
    public CustomerSearchCriteriaValidator()
    {
        RuleFor(criteria => criteria.ContactName)
            .MaximumLength(30).WithMessage("Contact name cannot be longer than 30 characters.")
            .When(criteria => !string.IsNullOrWhiteSpace(criteria.ContactName));
    }
}
