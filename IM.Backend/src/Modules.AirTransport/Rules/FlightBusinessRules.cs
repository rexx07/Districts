using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Air;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using Core.Infrastructure.Rules;
using Modules.AirTransport.Commands;
using Modules.AirTransport.Constants;

namespace Modules.AirTransport.Rules;

public class FlightBusinessRules: BaseBusinessRules
{
    private readonly IAirRepositoryManager _airRepositoryManager;

    public FlightBusinessRules(IAirRepositoryManager airRepositoryManager) =>
        _airRepositoryManager = airRepositoryManager;

    public async void FlightNotExists(CreateFlightCommand flight)
    {
        Flight? entity = await _airRepositoryManager.Flight.GetAsync(f => f.Id == flight.Id);

        if (entity is not null)
            throw new BusinessException(AirMessages.FlightAlreadyExists);
    }
    
    public async void FlightExists(Flight flight)
    {
        if (flight is null)
            throw new BusinessException(AirMessages.FlightNotExists);
    }
    
    public async void FlightsNotFound(IPaginate<Flight> flights)
    {
        if (flights.Count < 0)
            throw new BusinessException(AirMessages.FlightsNotFound);
    }

}