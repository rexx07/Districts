using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts;

namespace Core.Infrastructure.Persistence.Repositories;

public class InvoiceRepository : EfRepositoryBase<Invoice, BaseDbContext>, IInvoiceRepository
{
    public InvoiceRepository(BaseDbContext context)
        : base(context)
    {
    }
}