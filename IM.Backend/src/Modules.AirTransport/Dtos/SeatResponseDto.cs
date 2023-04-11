using Core.Domain.Enums;

namespace Modules.AirTransport.Dtos;

public record SeatResponseDto(long Id, string SeatNumber, SeatType Type, SeatClass Class, long FlightId);