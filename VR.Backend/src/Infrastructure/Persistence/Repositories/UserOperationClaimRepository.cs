using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, BaseDbContext>,
                                            IUserOperationClaimRepository
{
    public UserOperationClaimRepository(BaseDbContext context)
        : base(context)
    {
    }
}