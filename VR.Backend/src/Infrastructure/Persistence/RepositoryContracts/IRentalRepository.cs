using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IRentalRepository : IAsyncRepository<Rental>, IRepository<Rental>
{
}