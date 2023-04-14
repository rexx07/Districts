using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Land;
using Core.Domain.Enums;
using Core.Infrastructure.Rules;
using Modules.BaseApplication.Features.Cars.Constants;

namespace Modules.BaseApplication.Features.Cars.Rules;

public class CarBusinessRules : BaseBusinessRules
{
    private readonly ICarRepository _carRepository;

    public CarBusinessRules(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task CarIdShouldExistWhenSelected(int id)
    {
        Vehicle? result = await _carRepository.GetAsync(predicate: c => c.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(CarsMessages.CarNotExists);
    }

    public async Task CarCanNotBeMaintainWhenIsRented(int id)
    {
        Vehicle? car = await _carRepository.GetAsync(predicate: c => c.Id == id, enableTracking: false);
        if (car.CarState == VehicleState.Rented)
            throw new BusinessException(CarsMessages.CarCanNotBeMaintainWhenIsRented);
    }

    public async Task CarCanNotBeRentWhenIsInMaintenance(int carId)
    {
        Vehicle? car = await _carRepository.GetAsync(predicate: c => c.Id == carId, enableTracking: false);
        if (car.CarState == VehicleState.Maintenance)
            throw new BusinessException(CarsMessages.CarCanNotBeRentWhenIsInMaintenance);
    }

    public async Task CarCanNotBeRentWhenIsRented(int carId)
    {
        Vehicle? car = await _carRepository.GetAsync(predicate: c => c.Id == carId, enableTracking: false);
        if (car.CarState == VehicleState.Rented)
            throw new BusinessException(CarsMessages.CarCanNotBeRentWhenIsRented);
    }
}