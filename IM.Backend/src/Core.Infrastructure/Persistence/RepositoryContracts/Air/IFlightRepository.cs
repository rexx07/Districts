using Core.Domain.Entities.Air;
using Core.Infrastructure.Persistence.Repositories;

namespace Core.Infrastructure.Persistence.RepositoryContracts.Air;

public interface IFlightRepository: IAsyncRepository<Flight>, IRepository<Flight>
{
    
}