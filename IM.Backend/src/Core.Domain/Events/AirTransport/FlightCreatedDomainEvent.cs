using Core.CrossCuttingConcerns.Event;

namespace Core.Domain.Events.AirTransport;

public record FlightCreatedDomainEvent(long Id, string FlightNumber, long AircraftId, DateTime DepartureDate,
                                       long DepartureAirportId, DateTime ArriveDate, long ArriveAirportId, decimal DurationMinutes,
                                       DateTime FlightDate, Enums.FlightStatus Status, decimal Price, bool IsDeleted) : IDomainEvent;