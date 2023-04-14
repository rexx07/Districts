using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.Land;

namespace Core.Infrastructure.Persistence.Repositories.Land;

public class CarDamageRepository : EfRepositoryBase<VehicleDamage, BaseDbContext>, ICarDamageRepository
{
    public CarDamageRepository(BaseDbContext context)
        : base(context)
    {
    }
}