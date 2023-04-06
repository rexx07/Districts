using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface ICarRepository : IAsyncRepository<Car>, IRepository<Car>
{
}