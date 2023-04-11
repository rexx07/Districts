using Application.Features.FindeksCreditRates.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using Core.Infrastructure.Rules;

namespace Application.Features.FindeksCreditRates.Rules;

public class FindeksCreditRateBusinessRules : BaseBusinessRules
{
    private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;

    public FindeksCreditRateBusinessRules(IFindeksCreditRateRepository findeksCreditRateRepository)
    {
        _findeksCreditRateRepository = findeksCreditRateRepository;
    }

    public async Task FindeksCreditRateIdShouldExistWhenSelected(int id)
    {
        FindeksCreditRate? result =
            await _findeksCreditRateRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(FindeksCreditRatesMessages.FindeksCreditRateNotExists);
    }

    public Task FindeksCreditShouldBeExist(FindeksCreditRate? findeksCreditRate)
    {
        if (findeksCreditRate is null)
            throw new BusinessException(FindeksCreditRatesMessages.FindeksCreditRateNotExists);
        return Task.CompletedTask;
    }
}