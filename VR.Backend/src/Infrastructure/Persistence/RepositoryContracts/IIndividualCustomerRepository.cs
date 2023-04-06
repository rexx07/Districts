using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IIndividualCustomerRepository : IAsyncRepository<IndividualCustomer>, IRepository<IndividualCustomer>
{
}