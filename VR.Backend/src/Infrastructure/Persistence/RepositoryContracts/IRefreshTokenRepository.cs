using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken>, IRepository<RefreshToken>
{
}