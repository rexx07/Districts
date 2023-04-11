using Core.CrossCuttingConcerns.IdsGenerator;
using MediatR;

namespace Core.CrossCuttingConcerns.Event;

public interface IEvent : INotification
{
    long EventId => SnowFlakIdGenerator.NewId();
    public DateTime OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}
