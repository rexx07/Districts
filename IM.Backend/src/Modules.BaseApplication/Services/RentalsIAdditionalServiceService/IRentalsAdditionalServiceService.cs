using Core.Domain.Entities;

namespace Modules.BaseApplication.Services.RentalsIAdditionalServiceService;

public interface IRentalsAdditionalServiceService
{
    public Task<IList<RentalsAdditionalService>> AddManyByRentalIdAndAdditionalServices(
        int rentalId,
        IList<AdditionalService> additionalServices
    );
}