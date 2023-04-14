using Core.Domain.Entities;

namespace Modules.BaseApplication.Services.RentalService;

public interface IRentalService
{
    Task<IList<Rental>> GetAllByInDates(int carId, DateTime rentStartDate, DateTime rentEndDate);
}