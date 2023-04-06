using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IRentalBranchRepository : IAsyncRepository<RentalBranch>, IRepository<RentalBranch>
{
}