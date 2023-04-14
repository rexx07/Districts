using Core.Domain.Entities;
using Core.Domain.Entities.Land;

namespace Modules.BaseApplication.Services.CarService;

public interface ICarService
{
    public Task<Vehicle> GetById(int Id);
    public Task<Vehicle> PickUpCar(Rental rental);

    public Task<Vehicle?> GetAvailableCarToRent(int modelId, int rentStartRentalBranch, DateTime rentStartDate,
                                            DateTime rentEndDate);
}