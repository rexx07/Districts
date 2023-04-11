using Core.CrossCuttingConcerns.Event;

namespace Core.Domain.Entities.BaseEntities;

public interface IAggregate : IAudit, IVersion
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IEvent[] ClearDomainEvents();
}

public interface IAggregate<out T> : IAggregate
{
    T Id { get; }
}

public interface IVersion
{
    long Version { get; set; }
}