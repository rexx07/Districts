using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using Core.Domain.Entities.Land;
using Core.Domain.Enums;
using Modules.BaseApplication.Services.RentalService;

namespace Modules.BaseApplication.Services.CarService;

public class CarManager : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IRentalService _rentalService;

    public CarManager(ICarRepository carRepository, IRentalService rentalService)
    {
        _carRepository = carRepository;
        _rentalService = rentalService;
    }

    public async Task<Vehicle> GetById(int id)
    {
        Vehicle vehicle = await _carRepository.GetAsync(c => c.Id == id);
        if (vehicle == null)
            throw new BusinessException("The vehicle doesn't exist.");
        return vehicle;
    }

    public async Task<Vehicle> PickUpCar(Rental rental)
    {
        Vehicle vehicleToBeUpdate = await _carRepository.GetAsync(c => c.Id == rental.CarId);
        vehicleToBeUpdate.Kilometer += Convert.ToInt32(rental.RentEndKilometer - rental.RentStartKilometer);
        vehicleToBeUpdate.CarState = VehicleState.Available;
        Vehicle updatedVehicle = await _carRepository.UpdateAsync(vehicleToBeUpdate);
        return updatedVehicle;
    }

    public async Task<Vehicle?> GetAvailableCarToRent(int modelId, int rentStartRentalBranch, DateTime rentStartDate,
                                                  DateTime rentEndDate)
    {
        Vehicle? carToFind = await _carRepository.GetAsync(
                             predicate: c =>
                                 c.ModelId == modelId
                              && c.RentalBranchId == rentStartRentalBranch
                              && !c.Rentals.Any(r => r.RentStartDate <= rentStartDate && r.RentEndDate >= rentEndDate),
                             include: i => i.Include(i => i.Rentals)
                         );
        if (carToFind != null)
            return carToFind;
        throw new BusinessException("Available vehicle doesn't exist.");
    }
}