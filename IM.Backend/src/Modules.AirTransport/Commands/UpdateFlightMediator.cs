using Ardalis.GuardClauses;
using AutoMapper;
using Core.Domain.Entities.Air;
using Core.Domain.Enums;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using Core.Infrastructure.Requests;
using FluentValidation;
using MediatR;
using Modules.AirTransport.Dtos;
using Modules.AirTransport.Rules;
using Nest;

namespace Modules.AirTransport.Commands;

public sealed record UpdateFlightCommand(long Id, string FlightNumber, long AircraftId, long DepartureAirportId,
                                  DateTime DepartureDate, DateTime ArriveDate, long ArriveAirportId, decimal DurationMinutes, DateTime FlightDate,
                                  FlightStatus Status, bool IsDeleted, decimal Price) : ICommand<FlightResponseDto>
{
    public PageRequest PageRequest { get; set; }
    public string CacheKey => $"GetAvailableFlightQuery({PageRequest.Page},{PageRequest.PageSize})";

}

public sealed class UpdateFlightCommandHandler : ICommandHandler<UpdateFlightCommand, FlightResponseDto>
{
    private readonly IAirRepositoryManager _airRepositoryManager;
    private readonly IMapper _mapper;
    private readonly FlightBusinessRules _flightBusinessRules;

    public UpdateFlightCommandHandler(IAirRepositoryManager airRepositoryManager, IMapper mapper,
                                      FlightBusinessRules flightBusinessRules)
    {
        _airRepositoryManager = airRepositoryManager;
        _mapper = mapper;
        _flightBusinessRules = flightBusinessRules;
    }

    public async Task<FlightResponseDto> Handle(UpdateFlightCommand command, CancellationToken cancellationToken)
    {
        Flight? flight = await _airRepositoryManager.Flight.GetAsync(f => f.Id == command.Id,
                                                                     cancellationToken: cancellationToken);
        _flightBusinessRules.FlightExists(flight);
        
        flight.Update(command.Id, command.FlightNumber, command.AircraftId, command.DepartureAirportId, command.DepartureDate,
                      command.ArriveDate, command.ArriveAirportId, command.DurationMinutes, command.FlightDate, 
                      command.Status, command.Price, command.IsDeleted);

        Flight updateFlight = await _airRepositoryManager.Flight.UpdateAsync(flight, cancellationToken);

        return _mapper.Map<FlightResponseDto>(updateFlight);
    }
}

public class UpdateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
{
    public UpdateFlightCommandValidator(UpdateFlightCommand command)
    {
        Guard.Against.Null(command, parameterName: nameof(command));
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Status).Must(p => (p.GetType().IsEnum &&
                                          p == FlightStatus.Flying) ||
                                         p == FlightStatus.Canceled ||
                                         p == FlightStatus.Delay ||
                                         p == FlightStatus.Completed)
                              .WithMessage("Status must be Flying, Delay, Canceled or Completed");

        RuleFor(x => x.AircraftId).NotEmpty().WithMessage("AircraftId must be not empty");
        RuleFor(x => x.DepartureAirportId).NotEmpty().WithMessage("DepartureAirportId must be not empty");
        RuleFor(x => x.ArriveAirportId).NotEmpty().WithMessage("ArriveAirportId must be not empty");
        RuleFor(x => x.DurationMinutes).GreaterThan(0).WithMessage("DurationMinutes must be greater than 0");
        RuleFor(x => x.FlightDate).NotEmpty().WithMessage("FlightDate must be not empty");
    }
}
