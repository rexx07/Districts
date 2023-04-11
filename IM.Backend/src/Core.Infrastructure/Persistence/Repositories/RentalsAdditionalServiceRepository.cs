using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts;

namespace Core.Infrastructure.Persistence.Repositories;

public class RentalsAdditionalServiceRepository
    : EfRepositoryBase<RentalsAdditionalService, BaseDbContext>,
      IRentalsAdditionalServiceRepository
{
    public RentalsAdditionalServiceRepository(BaseDbContext context)
        : base(context)
    {
    }
}