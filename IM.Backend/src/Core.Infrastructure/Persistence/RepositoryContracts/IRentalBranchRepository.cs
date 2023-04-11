using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Repositories;

namespace Core.Infrastructure.Persistence.RepositoryContracts;

public interface IRentalBranchRepository : IAsyncRepository<RentalBranch>, IRepository<RentalBranch>
{
}