using Core.Domain.Entities.Bookings;
using Core.Infrastructure.Persistence.Repositories;

namespace Core.Infrastructure.Persistence.RepositoryContracts.Bookings;

public interface IBookingRepository: IAsyncRepository<Booking>, IRepository<Booking>
{
    
}