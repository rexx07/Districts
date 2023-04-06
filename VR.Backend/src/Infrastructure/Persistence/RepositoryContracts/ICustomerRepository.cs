using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface ICustomerRepository : IAsyncRepository<Customer>, IRepository<Customer>
{
}