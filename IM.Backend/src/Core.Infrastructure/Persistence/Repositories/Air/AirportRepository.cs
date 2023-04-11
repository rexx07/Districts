using Core.Domain.Entities.Air;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;

namespace Core.Infrastructure.Persistence.Repositories.Air;

public class AirportRepository: EfRepositoryBase<Airport, BaseDbContext>, IAirportRepository
{
    public AirportRepository(BaseDbContext context) : base(context)
    {
    }

}