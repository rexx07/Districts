using Ardalis.GuardClauses;
using AutoMapper;
using Core.Domain.Entities.Air;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using FluentValidation;
using MediatR;
using Modules.AirTransport.Dtos;
using Modules.AirTransport.Rules;

namespace Modules.AirTransport.Commands;

public sealed record ReserveSeatCommand(long FlightId, string SeatNumber) : ICommand<SeatResponseDto>;

public sealed record ReserveSeatCommandHandler : ICommandHandler<ReserveSeatCommand, SeatResponseDto>
{
    private readonly IAirRepositoryManager _airRepositoryManager;
    private readonly IMapper _mapper;
    private readonly SeatBusinessRules _seatBusinessRules;

    public ReserveSeatCommandHandler(IAirRepositoryManager airRepositoryManager, IMapper mapper, SeatBusinessRules
                                         seatBusinessRules)
    {
        _airRepositoryManager = airRepositoryManager;
        _mapper = mapper;
        _seatBusinessRules = seatBusinessRules;
    }

    public async Task<SeatResponseDto> Handle(ReserveSeatCommand command, CancellationToken cancellationToken)
    {
        Seat? seat = await _airRepositoryManager.Seat.GetAsync(predicate: s => s.SeatNumber == command.SeatNumber && s.FlightId == command.FlightId, cancellationToken: cancellationToken);
        _seatBusinessRules.SeatExists(seat);

        Seat reserveSeat = await seat.ReserveSeat(seat);
        Seat updatedSeat = await _airRepositoryManager.Seat.UpdateAsync(reserveSeat, cancellationToken);

        return _mapper.Map<SeatResponseDto>(updatedSeat);
    }
}

public sealed class ReserveSeatCommandHandlerValidator: AbstractValidator<ReserveSeatCommand>
{
    public ReserveSeatCommandHandlerValidator(ReserveSeatCommand command)
    {
        Guard.Against.Null(command, parameterName: nameof(command));
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.FlightId).NotEmpty().WithMessage("FlightId must not be empty");
        RuleFor(x => x.SeatNumber).NotEmpty().WithMessage("SeatNumber must not be empty");
    }
}