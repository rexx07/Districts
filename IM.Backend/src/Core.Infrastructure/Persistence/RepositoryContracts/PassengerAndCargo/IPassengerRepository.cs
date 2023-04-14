using Core.Domain.Entities.PassengerAndCargo;
using Core.Infrastructure.Persistence.Repositories;

namespace Core.Infrastructure.Persistence.RepositoryContracts.PassengerAndCargo;

public interface IPassengerRepository: IAsyncRepository<Passenger>, IRepository<Passenger>
{
    
}