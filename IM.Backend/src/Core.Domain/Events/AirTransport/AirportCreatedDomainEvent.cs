using Core.CrossCuttingConcerns.Event;

namespace Core.Domain.Events.AirTransport;

public record AirportCreatedDomainEvent(long Id, string Name, string Address, string Code, bool IsDeleted) 
    : IDomainEvent;