namespace Core.Infrastructure.Persistence.RepositoryContracts.Air;

public interface IAirRepositoryManager
{
    ISeatRepository Seat { get; }
    IAircraftRepository Aircraft { get; }
    IAirportRepository Airport { get; }
    IFlightRepository Flight { get; }
}