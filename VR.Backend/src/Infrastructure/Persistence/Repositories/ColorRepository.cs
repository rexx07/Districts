using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class ColorRepository : EfRepositoryBase<Color, BaseDbContext>, IColorRepository
{
    public ColorRepository(BaseDbContext context)
        : base(context)
    {
    }
}