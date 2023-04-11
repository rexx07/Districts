using Core.Domain.Enums;

namespace Modules.AirTransport.Dtos;

public record FlightResponseDto(long Id, string FlightNumber, long AircraftId, long DepartureAirportId,
                                DateTime DepartureDate, DateTime ArriveDate, long ArriveAirportId, decimal DurationMinutes, DateTime FlightDate,
                                FlightStatus Status, decimal Price);