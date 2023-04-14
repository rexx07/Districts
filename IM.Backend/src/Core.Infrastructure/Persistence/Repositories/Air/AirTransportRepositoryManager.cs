using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;

namespace Core.Infrastructure.Persistence.Repositories.Air;

public class AirTransportRepositoryManager: IAirRepositoryManager
{
    private readonly BaseDbContext _context;
    private readonly Lazy<ISeatRepository> _seatRepository;
    private readonly Lazy<IFlightRepository> _flightRepository;
    private readonly Lazy<IAirportRepository> _airportRepository;
    private readonly Lazy<IAircraftRepository> _aircraftRepository;

    public AirTransportRepositoryManager(BaseDbContext context)
    {
        _context = context;
        _seatRepository = new Lazy<ISeatRepository>(() => new SeatRepository(context));
        _flightRepository = new Lazy<IFlightRepository>(() => new FlightRepository(context));
        _airportRepository = new Lazy<IAirportRepository>(() => new AirportRepository(context));
        _aircraftRepository = new Lazy<IAircraftRepository>(() => new AircraftRepository(context));
    }

    public ISeatRepository Seat => _seatRepository.Value;
    public IAircraftRepository Aircraft => _aircraftRepository.Value;
    public IAirportRepository Airport => _airportRepository.Value;
    public IFlightRepository Flight => _flightRepository.Value;
}