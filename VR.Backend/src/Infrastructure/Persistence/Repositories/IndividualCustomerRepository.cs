using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class IndividualCustomerRepository : EfRepositoryBase<IndividualCustomer, BaseDbContext>,
                                            IIndividualCustomerRepository
{
    public IndividualCustomerRepository(BaseDbContext context)
        : base(context)
    {
    }
}