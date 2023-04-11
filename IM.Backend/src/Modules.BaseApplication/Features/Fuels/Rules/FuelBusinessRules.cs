using Application.Features.Fuels.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Rules;

namespace Application.Features.Fuels.Rules;

public class FuelBusinessRules : BaseBusinessRules
{
    private readonly IFuelRepository _fuelRepository;

    public FuelBusinessRules(IFuelRepository fuelRepository)
    {
        _fuelRepository = fuelRepository;
    }

    public async Task FuelIdShouldExistWhenSelected(int id)
    {
        Fuel? result = await _fuelRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(FuelsMessages.FuelNotExists);
    }

    public async Task FuelNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Fuel> result =
            await _fuelRepository.GetListAsync(predicate: b => b.Name == name, enableTracking: false);
        if (result.Items.Any())
            throw new BusinessException(FuelsMessages.FuelNameExists);
    }
}