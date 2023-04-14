using FluentValidation;

namespace Modules.BaseApplication.Features.Customers.Commands.Update;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(c => c.UserId).GreaterThan(0);
    }
}