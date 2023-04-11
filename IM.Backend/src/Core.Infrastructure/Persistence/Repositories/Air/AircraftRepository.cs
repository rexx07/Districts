using Core.Domain.Entities.Air;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;

namespace Core.Infrastructure.Persistence.Repositories.Air;

public class AircraftRepository: EfRepositoryBase<Aircraft, BaseDbContext>, IAircraftRepository
{
    public AircraftRepository(BaseDbContext context) : base(context)
    {
    }
}