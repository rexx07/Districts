using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class FindeksCreditRateRepository : EfRepositoryBase<FindeksCreditRate, BaseDbContext>,
                                           IFindeksCreditRateRepository
{
    public FindeksCreditRateRepository(BaseDbContext context)
        : base(context)
    {
    }
}