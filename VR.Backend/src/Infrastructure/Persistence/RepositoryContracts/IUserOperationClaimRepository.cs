using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim>, IRepository<UserOperationClaim>
{
}