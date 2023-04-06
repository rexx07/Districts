using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class RentalRepository : EfRepositoryBase<Rental, BaseDbContext>, IRentalRepository
{
    public RentalRepository(BaseDbContext context)
        : base(context)
    {
    }
}