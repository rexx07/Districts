using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.PassengerAndCargo;

namespace Core.Infrastructure.Persistence.Repositories.PassengerAndCargo;

public class PassengerAndCargoRepositoryManager: IPassengerAndCargoRepositoryManager
{
    private readonly BaseDbContext _context;
    private readonly Lazy<IPassengerRepository> _passengerRepository;

    public PassengerAndCargoRepositoryManager(BaseDbContext context)
    {
        _context = context;
        _passengerRepository = new Lazy<IPassengerRepository>(() => new PassengerRepository(context));
    }

    public IPassengerRepository Passenger => _passengerRepository.Value;
}