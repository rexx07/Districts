using Application.Services.RentalService;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Common.Exceptions.Types;
using Infrastructure.Persistence.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CarService;

public class CarManager : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarManager(ICarRepository carRepository, IRentalService rentalService)
    {
        _carRepository = carRepository;
    }

    public async Task<Car> GetById(int id)
    {
        Car car = await _carRepository.GetAsync(c => c.Id == id);
        if (car == null)
            throw new BusinessException("The car doesn't exist.");
        return car;
    }

    public async Task<Car> PickUpCar(Rental rental)
    {
        Car carToBeUpdate = await _carRepository.GetAsync(c => c.Id == rental.CarId);
        carToBeUpdate.Kilometer += Convert.ToInt32(rental.RentEndKilometer - rental.RentStartKilometer);
        carToBeUpdate.CarState = CarState.Available;
        Car updatedCar = await _carRepository.UpdateAsync(carToBeUpdate);
        return updatedCar;
    }

    public async Task<Car?> GetAvailableCarToRent(int modelId, int rentStartRentalBranch, DateTime rentStartDate,
                                                  DateTime rentEndDate)
    {
        Car? carToFind = await _carRepository.GetAsync(
                             predicate: c =>
                                 c.ModelId == modelId
                              && c.RentalBranchId == rentStartRentalBranch
                              && !c.Rentals.Any(r => r.RentStartDate <= rentStartDate && r.RentEndDate >= rentEndDate),
                             include: i => i.Include(i => i.Rentals)
                         );
        if (carToFind != null)
            return carToFind;
        throw new BusinessException("Available car doesn't exist.");
    }
}