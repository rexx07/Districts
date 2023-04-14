using Core.CrossCuttingConcerns.Event;
using Core.Domain.Enums;

namespace Core.Domain.Events.PassengerAndCargo;

public record PassengerRegistrationCompletedDomainEvent(long Id, string Name, string PassportNumber, 
                                                        PassengerType PassengerType, int Age, bool IsDeleted = false) : IDomainEvent;