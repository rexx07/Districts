using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Air;
using Core.Domain.Entities.Bookings;
using Core.Domain.Entities.PassengerAndCargo;
using Core.Infrastructure.Persistence.RepositoryContracts.Bookings;
using Core.Infrastructure.Rules;
using Modules.Bookings.Constants;

namespace Modules.Bookings.Rules;

public class BookingBusinessRules: BaseBusinessRules
{
    private readonly IBookingRepository _bookingRepository;

    public BookingBusinessRules(IBookingRepository bookingRepository) =>
        _bookingRepository = bookingRepository;

    public void FlightExists(Flight flight)
    {
        if (flight is null)
            throw new BusinessException(BookingMessages.FlightNotExists);
    }

    public void PassengerExists(Passenger passenger)
    {
        if (passenger is null)
        {
            throw new BusinessException(BookingMessages.PassengerNotExists);
        }
    }

    public void BookingNotExists(Booking booking)
    {
        if (booking is not null && !booking.IsDeleted)
            throw new BusinessException(BookingMessages.BookingExists);
    }
}