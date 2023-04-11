using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts;

namespace Core.Infrastructure.Persistence.Repositories;

public class RentalRepository : EfRepositoryBase<Rental, BaseDbContext>, IRentalRepository
{
    public RentalRepository(BaseDbContext context)
        : base(context)
    {
    }
}