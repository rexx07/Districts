using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class BrandRepository : EfRepositoryBase<Brand, BaseDbContext>, IBrandRepository
{
    public BrandRepository(BaseDbContext context)
        : base(context)
    {
    }
}