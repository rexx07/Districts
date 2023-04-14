using Core.CrossCuttingConcerns.Event;
using Core.Domain.Entities.BaseEntities;
using Core.Domain.ValueObjects;

namespace Core.Domain.Events.Bookings;

public record BookingCreatedDomainEvent(long Id, PassengerInfo PassengerInfo, Trip Trip) : Audit, IDomainEvent;