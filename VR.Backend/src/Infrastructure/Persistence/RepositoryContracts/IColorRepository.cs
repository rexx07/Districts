using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IColorRepository : IAsyncRepository<Color>, IRepository<Color>
{
}