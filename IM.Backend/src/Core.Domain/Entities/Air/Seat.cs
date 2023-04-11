﻿using Core.Domain.Entities.BaseEntities;
using Core.Domain.Enums;
using Core.Domain.Events.AirTransport;

namespace Core.Domain.Entities.Air;

public record Seat : Aggregate<long>
{
    public static Seat Create(long id, string seatNumber, Enums.SeatType type, Enums.SeatClass @class, long flightId,
                              bool isDeleted = false)
    {
        var seat = new Seat()
        {
            Id = id,
            Class = @class,
            Type = type,
            SeatNumber = seatNumber,
            FlightId = flightId,
            IsDeleted = isDeleted
        };

        var @event = new SeatCreatedDomainEvent(
            seat.Id,
            seat.SeatNumber,
            seat.Type,
            seat.Class,
            seat.FlightId,
            isDeleted);

        seat.AddDomainEvent(@event);

        return seat;
    }

    public Task<Seat> ReserveSeat(Seat seat)
    {
        seat.IsDeleted = true;
        seat.LastModified = DateTime.Now;

        var @event = new SeatReservedDomainEvent(
            seat.Id,
            seat.SeatNumber,
            seat.Type,
            seat.Class,
            seat.FlightId,
            seat.IsDeleted);

        seat.AddDomainEvent(@event);

        return Task.FromResult(this);
    }

    public string SeatNumber { get; private set; }
    public SeatType Type { get; private set; }
    public SeatClass Class { get; private set; }
    public long FlightId { get; private set; }
}