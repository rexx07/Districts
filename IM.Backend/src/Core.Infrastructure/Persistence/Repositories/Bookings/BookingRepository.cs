using Core.Domain.Entities.Bookings;
using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.RepositoryContracts.Bookings;

namespace Core.Infrastructure.Persistence.Repositories.Bookings;

public class BookingRepository: EfRepositoryBase<Booking, BaseDbContext>, IBookingRepository
{
    public BookingRepository(BaseDbContext context) : base(context)
    {
    }
}