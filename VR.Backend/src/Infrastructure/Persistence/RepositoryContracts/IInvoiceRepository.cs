using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IInvoiceRepository : IAsyncRepository<Invoice>, IRepository<Invoice>
{
}