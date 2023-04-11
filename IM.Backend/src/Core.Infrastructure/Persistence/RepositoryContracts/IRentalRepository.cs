using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Repositories;

namespace Core.Infrastructure.Persistence.RepositoryContracts;

public interface IRentalRepository : IAsyncRepository<Rental>, IRepository<Rental>
{
}