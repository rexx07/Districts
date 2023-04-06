using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class CarRepository : EfRepositoryBase<Car, BaseDbContext>, ICarRepository
{
    public CarRepository(BaseDbContext context)
        : base(context)
    {
    }
}