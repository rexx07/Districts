using Core.Domain.Entities.BaseEntities;
using Core.Domain.Events.AirTransport;

namespace Core.Domain.Entities.Air;

public record Aircraft : Aggregate<long>
{
    public string Name { get; private set; }
    public string Model { get; private set; }
    public int ManufacturingYear { get; set; }
    
    public static Aircraft Create(long id, string name, string model, int manufacturingYear, bool isDeleted = false)
    {
        var aircraft = new Aircraft
        {
            Id = id,
            Name = name,
            Model = model,
            ManufacturingYear = manufacturingYear
        };

        var @event = new AircraftCreatedDomainEvent(
            aircraft.Id,
            aircraft.Name,
            aircraft.Model,
            aircraft.ManufacturingYear,
            isDeleted);

        aircraft.AddDomainEvent(@event);

        return aircraft;
    }
}