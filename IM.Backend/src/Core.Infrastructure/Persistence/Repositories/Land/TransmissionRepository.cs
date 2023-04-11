using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.Land;

namespace Core.Infrastructure.Persistence.Repositories.Land;

public class TransmissionRepository : EfRepositoryBase<Transmission, BaseDbContext>, ITransmissionRepository
{
    public TransmissionRepository(BaseDbContext context)
        : base(context)
    {
    }
}