using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IModelRepository : IAsyncRepository<Model>, IRepository<Model>
{
}