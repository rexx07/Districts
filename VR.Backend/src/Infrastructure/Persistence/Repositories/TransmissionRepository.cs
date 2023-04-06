using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.RepositoryContracts;

namespace Infrastructure.Persistence.Repositories;

public class TransmissionRepository : EfRepositoryBase<Transmission, BaseDbContext>, ITransmissionRepository
{
    public TransmissionRepository(BaseDbContext context)
        : base(context)
    {
    }
}