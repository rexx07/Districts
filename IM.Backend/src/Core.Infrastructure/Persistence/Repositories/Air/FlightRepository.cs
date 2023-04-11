using Core.Domain.Entities.Air;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;

namespace Core.Infrastructure.Persistence.Repositories.Air;

public class FlightRepository: EfRepositoryBase<Flight, BaseDbContext>, IFlightRepository
{
    public FlightRepository(BaseDbContext context) : base(context)
    {
    }
}