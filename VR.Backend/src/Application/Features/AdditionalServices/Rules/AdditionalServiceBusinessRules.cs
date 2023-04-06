using Application.Features.AdditionalServices.Constants;
using Application.Rules;
using Domain.Entities;
using Infrastructure.Common.Exceptions.Types;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;

namespace Application.Features.AdditionalServices.Rules;

public class AdditionalServiceBusinessRules : BaseBusinessRules
{
    private readonly IAdditionalServiceRepository _additionalServiceRepository;

    public AdditionalServiceBusinessRules(IAdditionalServiceRepository additionalServiceRepository)
    {
        _additionalServiceRepository = additionalServiceRepository;
    }

    public async Task AdditionalServiceIdShouldExistWhenSelected(int id)
    {
        AdditionalService? result =
            await _additionalServiceRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(AdditionalServicesMessages.AdditionalServiceNotExists);
    }

    public async Task AdditionalServiceNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<AdditionalService> result = await _additionalServiceRepository.GetListAsync(
                                                  predicate: a => a.Name == name,
                                                  enableTracking: false
                                              );
        if (result.Items.Any())
            throw new BusinessException(AdditionalServicesMessages.AdditionalServiceNameExists);
    }
}