using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts;

namespace Core.Infrastructure.Persistence.Repositories;

public class RentalBranchRepository : EfRepositoryBase<RentalBranch, BaseDbContext>, IRentalBranchRepository
{
    public RentalBranchRepository(BaseDbContext context)
        : base(context)
    {
    }
}