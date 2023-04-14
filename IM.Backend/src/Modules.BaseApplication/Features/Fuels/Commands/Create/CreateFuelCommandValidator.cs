using FluentValidation;

namespace Modules.BaseApplication.Features.Fuels.Commands.Create;

public class CreateFuelCommandValidator : AbstractValidator<CreateFuelCommand>
{
    public CreateFuelCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
    }
}