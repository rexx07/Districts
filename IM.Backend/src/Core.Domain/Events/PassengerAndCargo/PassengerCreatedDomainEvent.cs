using Core.CrossCuttingConcerns.Event;

namespace Core.Domain.Events.PassengerAndCargo;

public record PassengerCreatedDomainEvent(long Id, string Name, string PassportNumber, bool IsDeleted = false) 
    : IDomainEvent;