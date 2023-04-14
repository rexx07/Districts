using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Rules;
using Modules.BaseApplication.Features.CarDamages.Constants;

namespace Modules.BaseApplication.Features.CarDamages.Rules;

public class CarDamageBusinessRules : BaseBusinessRules
{
    private readonly ICarDamageRepository _carDamageRepository;

    public CarDamageBusinessRules(ICarDamageRepository carDamageRepository)
    {
        _carDamageRepository = carDamageRepository;
    }

    public async Task CarDamageIdShouldExistWhenSelected(int id)
    {
        VehicleDamage? result = await _carDamageRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(CarDamagesMessages.CarDamageNotExists);
    }
}