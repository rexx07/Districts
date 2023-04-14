using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Security;
using Core.Infrastructure.Rules;
using Modules.BaseApplication.Features.UserOperationClaims.Constants;

namespace Modules.BaseApplication.Features.UserOperationClaims.Rules;

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