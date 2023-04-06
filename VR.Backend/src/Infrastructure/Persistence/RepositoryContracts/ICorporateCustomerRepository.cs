using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface ICorporateCustomerRepository : IAsyncRepository<CorporateCustomer>, IRepository<CorporateCustomer>
{
}