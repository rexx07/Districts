using Core.Domain.Entities.PassengerAndCargo;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.PassengerAndCargo;

namespace Core.Infrastructure.Persistence.Repositories.PassengerAndCargo;

public class PassengerRepository: EfRepositoryBase<Passenger, BaseDbContext>, IPassengerRepository
{
    public PassengerRepository(BaseDbContext context) : base(context)
    {
    }
}