using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.Land;

namespace Core.Infrastructure.Persistence.Repositories.Land;

public class ModelRepository : EfRepositoryBase<Model, BaseDbContext>, IModelRepository
{
    public ModelRepository(BaseDbContext context)
        : base(context)
    {
    }
}