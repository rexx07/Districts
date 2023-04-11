using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts;

namespace Core.Infrastructure.Persistence.Repositories;

public class AdditionalServiceRepository : EfRepositoryBase<AdditionalService, BaseDbContext>,
                                           IAdditionalServiceRepository
{
    public AdditionalServiceRepository(BaseDbContext context)
        : base(context)
    {
    }
}