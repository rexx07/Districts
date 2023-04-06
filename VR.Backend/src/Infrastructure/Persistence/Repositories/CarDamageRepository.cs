using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class CarDamageRepository : EfRepositoryBase<CarDamage, BaseDbContext>, ICarDamageRepository
{
    public CarDamageRepository(BaseDbContext context)
        : base(context)
    {
    }
}