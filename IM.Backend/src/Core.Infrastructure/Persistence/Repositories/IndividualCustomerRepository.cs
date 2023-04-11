using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts;

namespace Core.Infrastructure.Persistence.Repositories;

public class IndividualCustomerRepository : EfRepositoryBase<IndividualCustomer, BaseDbContext>,
                                            IIndividualCustomerRepository
{
    public IndividualCustomerRepository(BaseDbContext context)
        : base(context)
    {
    }
}