using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts;

namespace Core.Infrastructure.Persistence.Repositories;

public class CustomerRepository : EfRepositoryBase<Customer, BaseDbContext>, ICustomerRepository
{
    public CustomerRepository(BaseDbContext context)
        : base(context)
    {
    }
}