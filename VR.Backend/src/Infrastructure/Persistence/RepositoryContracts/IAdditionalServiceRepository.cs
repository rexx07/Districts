using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IAdditionalServiceRepository : IAsyncRepository<AdditionalService>, IRepository<AdditionalService>
{
}