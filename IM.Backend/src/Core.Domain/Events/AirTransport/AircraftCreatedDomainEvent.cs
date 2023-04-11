using Core.CrossCuttingConcerns.Event;

namespace Core.Domain.Events.AirTransport;

public record AircraftCreatedDomainEvent(long Id, string Name, string Model, int ManufacturingYear, bool IsDeleted)
    : IDomainEvent;