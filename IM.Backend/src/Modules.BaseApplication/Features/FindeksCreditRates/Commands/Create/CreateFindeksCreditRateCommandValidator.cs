using FluentValidation;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Commands.Create;

public class CreateFindeksCreditRateCommandValidator : AbstractValidator<CreateFindeksCreditRateCommand>
{
    public CreateFindeksCreditRateCommandValidator()
    {
        RuleFor(f => f.Score).GreaterThanOrEqualTo(Convert.ToInt16(0)).LessThanOrEqualTo(Convert.ToInt16(1900));
    }
}