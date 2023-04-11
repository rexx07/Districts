using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.Land;

namespace Core.Infrastructure.Persistence.Repositories.Land;

public class CarRepository : EfRepositoryBase<Car, BaseDbContext>, ICarRepository
{
    public CarRepository(BaseDbContext context)
        : base(context)
    {
    }
}