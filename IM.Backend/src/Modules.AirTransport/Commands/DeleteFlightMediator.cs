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

public sealed record DeleteFlightCommand(long Id) : ICommand<FlightResponseDto>;

public sealed class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, FlightResponseDto>
{
    private readonly IAirRepositoryManager _airRepositoryManager;
    private readonly IMapper _mapper;
    private readonly FlightBusinessRules _flightBusinessRules;

    public DeleteFlightCommandHandler(IAirRepositoryManager airRepositoryManager, IMapper mapper,
                                      FlightBusinessRules flightBusinessRules)
    {
        _airRepositoryManager = airRepositoryManager;
        _mapper = mapper;
        _flightBusinessRules = flightBusinessRules;
    }

    public async Task<FlightResponseDto> Handle(DeleteFlightCommand command, CancellationToken cancellationToken)
    {
        Flight? flight = await _airRepositoryManager.Flight.GetAsync(predicate: f => f.Id == command.Id, 
                                                                     cancellationToken: cancellationToken);
        _flightBusinessRules.FlightExists(flight);

        Flight deleteFlight = _airRepositoryManager.Flight.Delete(flight);
        
        deleteFlight .Delete(deleteFlight.Id, deleteFlight.FlightNumber, deleteFlight.AircraftId, deleteFlight.DepartureAirportId,
                      deleteFlight.DepartureDate, deleteFlight.ArriveDate, deleteFlight.ArriveAirportId, 
                      deleteFlight.DurationMinutes, deleteFlight.FlightDate, deleteFlight.Status, deleteFlight.Price);

        return _mapper.Map<FlightResponseDto>(deleteFlight);
    }
}

public sealed class DeleteFlightCommandValidator : AbstractValidator<DeleteFlightCommand>
{
    public DeleteFlightCommandValidator(DeleteFlightCommand command)
    {
        Guard.Against.Null(command, parameterName: nameof(command));
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id).NotEmpty();
    }
}
