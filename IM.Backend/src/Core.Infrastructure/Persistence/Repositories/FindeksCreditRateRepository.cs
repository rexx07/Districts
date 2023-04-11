using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts;

namespace Core.Infrastructure.Persistence.Repositories;

public class FindeksCreditRateRepository : EfRepositoryBase<FindeksCreditRate, BaseDbContext>,
                                           IFindeksCreditRateRepository
{
    public FindeksCreditRateRepository(BaseDbContext context)
        : base(context)
    {
    }
}