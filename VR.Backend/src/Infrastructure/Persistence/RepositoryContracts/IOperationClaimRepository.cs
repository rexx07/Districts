using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IOperationClaimRepository : IAsyncRepository<OperationClaim>, IRepository<OperationClaim>
{
}