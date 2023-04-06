using Application.Features.UserOperationClaims.Constants;
using Application.Rules;
using Domain.Entities;
using Infrastructure.Common.Exceptions.Types;
using Infrastructure.Persistence.RepositoryContracts;

namespace Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task UserOperationClaimIdShouldExistWhenSelected(int id)
    {
        UserOperationClaim? result =
            await _userOperationClaimRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(UserOperationClaimsMessages.UserOperationClaimNotExists);
    }
}