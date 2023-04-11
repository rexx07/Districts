using Core.CrossCuttingConcerns.Event;

namespace Core.Domain.Events.AirTransport;

public record SeatCreatedDomainEvent(long Id, string SeatNumber, Enums.SeatType Type, Enums.SeatClass Class, 
                                     long FlightId, bool IsDeleted) : IDomainEvent;