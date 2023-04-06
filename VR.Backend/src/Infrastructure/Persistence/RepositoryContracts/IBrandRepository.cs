using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IBrandRepository : IAsyncRepository<Brand>, IRepository<Brand>
{
}