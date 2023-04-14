using FluentValidation;

namespace Modules.BaseApplication.Features.Fuels.Commands.Update;

public class UpdateFuelCommandValidator : AbstractValidator<UpdateFuelCommand>
{
    public UpdateFuelCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
    }
}