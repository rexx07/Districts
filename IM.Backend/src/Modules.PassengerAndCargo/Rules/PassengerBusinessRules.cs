using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.PassengerAndCargo;
using Core.Infrastructure.Persistence.RepositoryContracts.PassengerAndCargo;
using Core.Infrastructure.Rules;
using Modules.PassengerAndCargo.Commands;
using Modules.PassengerAndCargo.Constants;

namespace Modules.PassengerAndCargo.Rules;

public sealed class PassengerBusinessRules: BaseBusinessRules
{
    private readonly IPassengerAndCargoRepositoryManager _passengerAndCargo;

    public PassengerBusinessRules(IPassengerAndCargoRepositoryManager passengerAndCargo) =>
        _passengerAndCargo = passengerAndCargo;
    
    public async void PassengerExists(Passenger passenger)
    {
        if (passenger is null)
            throw new BusinessException(PassengerAndCargoMessages.PassengerNotExists);
    }
}