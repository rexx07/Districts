using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface ITransmissionRepository : IAsyncRepository<Transmission>, IRepository<Transmission>
{
}