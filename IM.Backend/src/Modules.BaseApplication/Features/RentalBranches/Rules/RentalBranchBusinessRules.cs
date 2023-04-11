using Application.Features.RentalBranches.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using Core.Infrastructure.Rules;

namespace Application.Features.RentalBranches.Rules;

public class RentalBranchBusinessRules : BaseBusinessRules
{
    private readonly IRentalBranchRepository _rentalBranchRepository;

    public RentalBranchBusinessRules(IRentalBranchRepository rentalBranchRepository)
    {
        _rentalBranchRepository = rentalBranchRepository;
    }

    public async Task RentalBranchIdShouldExistWhenSelected(int id)
    {
        RentalBranch? result =
            await _rentalBranchRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(RentalBranchesMessages.RentalBranchNotExists);
    }
}