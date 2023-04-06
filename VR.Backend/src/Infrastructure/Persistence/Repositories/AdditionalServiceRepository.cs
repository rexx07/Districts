using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class AdditionalServiceRepository : EfRepositoryBase<AdditionalService, BaseDbContext>,
                                           IAdditionalServiceRepository
{
    public AdditionalServiceRepository(BaseDbContext context)
        : base(context)
    {
    }
}