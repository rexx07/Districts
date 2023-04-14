using FluentValidation;

namespace Modules.BaseApplication.Features.RentalBranches.Commands.Update;

public class UpdateRentalBranchCommandValidator : AbstractValidator<UpdateRentalBranchCommand>
{
    public UpdateRentalBranchCommandValidator()
    {
        RuleFor(c => c.City).NotEmpty();
    }
}