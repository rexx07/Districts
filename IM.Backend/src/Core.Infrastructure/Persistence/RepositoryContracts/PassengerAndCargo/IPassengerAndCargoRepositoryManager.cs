namespace Core.Infrastructure.Persistence.RepositoryContracts.PassengerAndCargo;

public interface IPassengerAndCargoRepositoryManager
{
    IPassengerRepository Passenger { get; }
}