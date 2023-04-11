using Application.Features.CarDamages.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Rules;

namespace Application.Features.CarDamages.Rules;

public class CarDamageBusinessRules : BaseBusinessRules
{
    private readonly ICarDamageRepository _carDamageRepository;

    public CarDamageBusinessRules(ICarDamageRepository carDamageRepository)
    {
        _carDamageRepository = carDamageRepository;
    }

    public async Task CarDamageIdShouldExistWhenSelected(int id)
    {
        CarDamage? result = await _carDamageRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(CarDamagesMessages.CarDamageNotExists);
    }
}