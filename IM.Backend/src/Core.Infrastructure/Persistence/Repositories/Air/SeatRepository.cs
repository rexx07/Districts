using Core.Domain.Entities.Air;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;

namespace Core.Infrastructure.Persistence.Repositories.Air;

public class SeatRepository: EfRepositoryBase<Seat, BaseDbContext>, ISeatRepository
{
    public SeatRepository(BaseDbContext context) : base(context)
    {
    }
}