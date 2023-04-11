using Core.CrossCuttingConcerns.Event;

namespace Core.Domain.Entities.BaseEntities;

public abstract record Aggregate : Aggregate<long>
{
}

public abstract record Aggregate<TId> : Audit, IAggregate<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IEvent[] ClearDomainEvents()
    {
        IEvent[] dequeuedEvents = _domainEvents.ToArray();

        _domainEvents.Clear();

        return dequeuedEvents;
    }

    public long Version { get; set; }

    public TId Id { get; set;  }
}