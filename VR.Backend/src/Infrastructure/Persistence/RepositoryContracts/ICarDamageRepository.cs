using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface ICarDamageRepository : IAsyncRepository<CarDamage>, IRepository<CarDamage>
{
}