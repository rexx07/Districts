using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class RentalsAdditionalServiceRepository
    : EfRepositoryBase<RentalsAdditionalService, BaseDbContext>,
      IRentalsAdditionalServiceRepository
{
    public RentalsAdditionalServiceRepository(BaseDbContext context)
        : base(context)
    {
    }
}