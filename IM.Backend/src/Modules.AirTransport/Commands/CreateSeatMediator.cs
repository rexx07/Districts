using AutoMapper;
using Core.CrossCuttingConcerns.IdsGenerator;
using Core.Domain.Entities.Air;
using Core.Domain.Enums;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using MediatR;
using Modules.AirTransport.Dtos;
using Modules.AirTransport.Rules;

namespace Modules.AirTransport.Commands;

public sealed record CreateSeatCommand(string SeatNumber, SeatType Type, SeatClass Class, long FlightId) : ICommand<SeatResponseDto>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

public sealed class CreateSeatCommandHandler : IRequestHandler<CreateSeatCommand, SeatResponseDto>
{
    private readonly IAirRepositoryManager _airRepositoryManager;
    private readonly IMapper _mapper;
    private readonly SeatBusinessRules _seatBusinessRules;

    public CreateSeatCommandHandler(IAirRepositoryManager airRepositoryManager, IMapper mapper,
                                    SeatBusinessRules seatBusinessRules)
    {
        _airRepositoryManager = airRepositoryManager;
        _mapper = mapper;
        _seatBusinessRules = seatBusinessRules;
    }

    public async Task<SeatResponseDto> Handle(CreateSeatCommand command, CancellationToken cancellationToken)
    {
        _seatBusinessRules.Check(command);

        Seat seatEntity = Seat.Create(command.Id, command.SeatNumber, command.Type, command.Class, command.FlightId);
        Seat newSeat = await _airRepositoryManager.Seat.AddAsync(seatEntity, cancellationToken);

        return _mapper.Map<SeatResponseDto>(newSeat);
    }
}