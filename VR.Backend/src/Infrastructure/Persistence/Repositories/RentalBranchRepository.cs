using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class RentalBranchRepository : EfRepositoryBase<RentalBranch, BaseDbContext>, IRentalBranchRepository
{
    public RentalBranchRepository(BaseDbContext context)
        : base(context)
    {
    }
}