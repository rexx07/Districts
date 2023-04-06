using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IUserRepository : IAsyncRepository<User>, IRepository<User>
{
}