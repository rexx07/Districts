using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : EfRepositoryBase<Customer, BaseDbContext>, ICustomerRepository
{
    public CustomerRepository(BaseDbContext context)
        : base(context)
    {
    }
}