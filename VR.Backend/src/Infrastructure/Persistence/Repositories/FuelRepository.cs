using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class FuelRepository : EfRepositoryBase<Fuel, BaseDbContext>, IFuelRepository
{
    public FuelRepository(BaseDbContext context)
        : base(context)
    {
    }
}