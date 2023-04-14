using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Rules;
using Modules.BaseApplication.Features.Rentals.Constants;

namespace Modules.BaseApplication.Features.Rentals.Rules;

public class RentalBusinessRules : BaseBusinessRules
{
    private readonly IRentalRepository _rentalRepository;

    public RentalBusinessRules(IRentalRepository rentalRepository, ICarRepository carRepository)
    {
        _rentalRepository = rentalRepository;
    }

    public async Task RentalIdShouldExistWhenSelected(int id)
    {
        Rental? result = await _rentalRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(RentalsMessages.RentalNotExists);
    }

    public async Task RentalCanNotBeUpdateWhenThereIsARentedCarInDate(int id, int carId, DateTime rentStartDate,
                                                                      DateTime rentEndDate)
    {
        IPaginate<Rental> rentals = await _rentalRepository.GetListAsync(
                                        predicate: r =>
                                            r.Id != id && r.CarId == carId && r.RentEndDate >= rentStartDate &&
                                            r.RentStartDate <= rentEndDate,
                                        enableTracking: false
                                    );
        if (rentals.Items.Any())
            throw new BusinessException(RentalsMessages.RentalCanNotBeUpdatedWhenThereIsAnotherRentedCarForTheDate);
    }

    public Task RentalCanNotBeCreatedWhenCustomerFindeksScoreLowerThanCarMinFindeksScore(
        short customerFindeksCreditRate,
        short carMinFindeksCreditRate
    )
    {
        if (customerFindeksCreditRate < carMinFindeksCreditRate)
            throw new BusinessException(RentalsMessages
                                            .RentalCanNotBeCreatedWhenCustomerFindeksCreditScoreLowerThanCarMinFindeksScore);
        return Task.CompletedTask;
    }
}