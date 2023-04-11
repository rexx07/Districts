using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts;

namespace Core.Infrastructure.Persistence.Repositories;

public class CorporateCustomerRepository : EfRepositoryBase<CorporateCustomer, BaseDbContext>,
                                           ICorporateCustomerRepository
{
    public CorporateCustomerRepository(BaseDbContext context)
        : base(context)
    {
    }
}