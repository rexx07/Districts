using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Rules;
using Modules.BaseApplication.Features.Fuels.Constants;

namespace Modules.BaseApplication.Features.Fuels.Rules;

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