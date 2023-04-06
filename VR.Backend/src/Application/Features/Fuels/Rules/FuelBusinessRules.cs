using Application.Features.Fuels.Constants;
using Application.Rules;
using Domain.Entities;
using Infrastructure.Common.Exceptions.Types;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;

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