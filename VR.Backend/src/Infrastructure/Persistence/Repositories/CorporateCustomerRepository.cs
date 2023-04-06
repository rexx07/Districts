using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class CorporateCustomerRepository : EfRepositoryBase<CorporateCustomer, BaseDbContext>,
                                           ICorporateCustomerRepository
{
    public CorporateCustomerRepository(BaseDbContext context)
        : base(context)
    {
    }
}