using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class InvoiceRepository : EfRepositoryBase<Invoice, BaseDbContext>, IInvoiceRepository
{
    public InvoiceRepository(BaseDbContext context)
        : base(context)
    {
    }
}