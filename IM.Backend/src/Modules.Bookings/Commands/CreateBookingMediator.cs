using Ardalis.GuardClauses;
using AutoMapper;
using Core.CrossCuttingConcerns.IdsGenerator;
using Core.Domain.Entities.Air;
using Core.Domain.Entities.Bookings;
using Core.Domain.Entities.PassengerAndCargo;
using Core.Domain.ValueObjects;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using Core.Infrastructure.Persistence.RepositoryContracts.Bookings;
using Core.Infrastructure.Persistence.RepositoryContracts.PassengerAndCargo;
using MediatR;
using Modules.Bookings.Dtos;
using Modules.Bookings.Rules;

namespace Modules.Bookings.Commands;

public sealed record CreateBookingCommand(long PassengerId, long FlightId, string Description) : ICommand<BookingResponseDto>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

public sealed class CreateBookingCommandHandler : ICommandHandler<CreateBookingCommand, BookingResponseDto>
{
    private readonly IAirRepositoryManager _airRepository;
    private readonly IPassengerAndCargoRepositoryManager _passengerAndCargo;
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;
    private readonly BookingBusinessRules _bookingBusinessRules;

    public CreateBookingCommandHandler(IBookingRepository bookingRepository, IMapper mapper,
                                       BookingBusinessRules bookingBusinessRules,
                                       IAirRepositoryManager airRepositoryManager,
                                       IPassengerAndCargoRepositoryManager passengerAndCargo)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
        _bookingBusinessRules = bookingBusinessRules;
        _airRepository = airRepositoryManager;
        _passengerAndCargo = passengerAndCargo;
    }

    public async Task<BookingResponseDto> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, parameterName: nameof(command));
        Flight? flight = await _airRepository.Flight.GetAsync(predicate: f => f.Id == command.FlightId, 
                                                                     cancellationToken: cancellationToken);
        _bookingBusinessRules.FlightExists(flight);

        Passenger? passenger = await _passengerAndCargo.Passenger.GetAsync(predicate: p => p.Id == command.PassengerId,
                                                                           cancellationToken: cancellationToken);
        _bookingBusinessRules.PassengerExists(passenger);
        
        IPaginate<Seat> emptySeats = await _airRepository.Seat.GetListAsync(
                                    predicate: s => s.FlightId == command.FlightId && !s.IsDeleted,
                                    orderBy: s => s.OrderBy(s => s.SeatNumber),
                                    cancellationToken: cancellationToken);
        
        Seat emptySeat = emptySeats.Items.FirstOrDefault();

        Booking? booking = await _bookingRepository.GetAsync(predicate: b => b.Id == command.Id, 
                                                             cancellationToken: cancellationToken);
        
    }
}