using FluentValidation;

namespace Modules.BaseApplication.Features.Customers.Commands.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(c => c.UserId).GreaterThan(0);
    }
}