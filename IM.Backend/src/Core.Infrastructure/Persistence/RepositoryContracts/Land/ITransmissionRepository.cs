using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Repositories;

namespace Core.Infrastructure.Persistence.RepositoryContracts.Land;

public interface ITransmissionRepository : IAsyncRepository<Transmission>, IRepository<Transmission>
{
}