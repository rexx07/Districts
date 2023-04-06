using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IFuelRepository : IAsyncRepository<Fuel>, IRepository<Fuel>
{
}