﻿using Core.Domain.Entities;

namespace Modules.BaseApplication.Services.RentalsIAdditionalServiceService;

public class RentalsAdditionalServiceManager : IRentalsAdditionalServiceService
{
    private readonly IRentalsAdditionalServiceRepository _additionalServiceRepository;

    public RentalsAdditionalServiceManager(IRentalsAdditionalServiceRepository additionalServiceRepository)
    {
        _additionalServiceRepository = additionalServiceRepository;
    }

    public async Task<IList<RentalsAdditionalService>> AddManyByRentalIdAndAdditionalServices(
        int rentalId,
        IList<AdditionalService> additionalServices
    )
    {
        IList<RentalsAdditionalService> rentalsAdditionalServices = additionalServices
                                                                    .Select(a => new RentalsAdditionalService
                                                                    {
                                                                        RentalId = rentalId, AdditionalServiceId = a.Id
                                                                    })
                                                                    .ToList();

        foreach (RentalsAdditionalService rentalsAdditionalService in rentalsAdditionalServices)
            await _additionalServiceRepository.AddAsync(rentalsAdditionalService);

        return rentalsAdditionalServices;
    }
}