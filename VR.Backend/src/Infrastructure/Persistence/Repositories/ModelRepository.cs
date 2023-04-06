using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class ModelRepository : EfRepositoryBase<Model, BaseDbContext>, IModelRepository
{
    public ModelRepository(BaseDbContext context)
        : base(context)
    {
    }
}