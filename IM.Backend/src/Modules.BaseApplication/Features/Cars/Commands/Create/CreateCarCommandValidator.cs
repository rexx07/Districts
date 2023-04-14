using FluentValidation;
using Modules.BaseApplication.Features.Cars.Validations;

namespace Modules.BaseApplication.Features.Cars.Commands.Create;

public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(c => c.ModelYear).GreaterThan((short)1900);
        RuleFor(c => c.Plate).NotEmpty().Must(CarCustomValidationRules.IsTurkeyPlate)
                             .WithMessage("Plate is not valid.");
    }
}