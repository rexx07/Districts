using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Security;
using Core.Infrastructure.Rules;
using Modules.BaseApplication.Features.OperationClaims.Constants;

namespace Modules.BaseApplication.Features.OperationClaims.Rules;

public class OperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task OperationClaimIdShouldExistWhenSelected(int id)
    {
        OperationClaim? result =
            await _operationClaimRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(OperationClaimsMessages.OperationClaimNotExists);
    }
}