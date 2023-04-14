using Core.Domain.Enums;
using Modules.BaseApplication.Dtos;

namespace Modules.AirTransport.Dtos;

public record SeatResponseDto(long Id, string SeatNumber, SeatType Type, SeatClass Class, long FlightId): IDto;