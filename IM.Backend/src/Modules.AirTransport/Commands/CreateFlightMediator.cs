﻿using Ardalis.GuardClauses;
using AutoMapper;
using Core.CrossCuttingConcerns.IdsGenerator;
using Core.Domain.Entities.Air;
using Core.Domain.Enums;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using FluentValidation;
using MediatR;
using Modules.AirTransport.Dtos;
using Modules.AirTransport.Rules;

namespace Modules.AirTransport.Commands;

public sealed record CreateFlightCommand(string FlightNumber, long AircraftId, long DepartureAirportId,
                                  DateTime DepartureDate, DateTime ArriveDate, long ArriveAirportId,
                                  decimal DurationMinutes, DateTime FlightDate, FlightStatus Status, decimal Price) 
    : ICommand<FlightResponseDto>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

public sealed record CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, FlightResponseDto>
{
    private readonly IAirRepositoryManager _airRepositoryManager;
    private readonly IMapper _mapper;
    private readonly FlightBusinessRules _flightBusinessRules;

    public CreateFlightCommandHandler(IAirRepositoryManager airRepositoryManager, IMapper mapper,
                                      FlightBusinessRules flightBusinessRules)
    {
        _airRepositoryManager = airRepositoryManager;
        _mapper = mapper;
        _flightBusinessRules = flightBusinessRules;
    }


    public async Task<FlightResponseDto> Handle(CreateFlightCommand command, CancellationToken cancellationToken)
    {
        _flightBusinessRules.FlightNotExists(command);
        
        Flight flightEntity = Flight.Create(command.Id, command.FlightNumber, command.AircraftId, command.DepartureAirportId, 
                                            command.DepartureDate, command.ArriveDate, command.ArriveAirportId, 
                                            command.DurationMinutes, command.FlightDate, command.Status, command.Price);

        Flight newFlight = await _airRepositoryManager.Flight.AddAsync(flightEntity, cancellationToken);

        return _mapper.Map<FlightResponseDto>(newFlight);
    }
}

public sealed class CreateFlightCommandValidator: AbstractValidator<CreateFlightCommand>
{
    public CreateFlightCommandValidator(CreateFlightCommand command)
    {
        Guard.Against.Null(command, parameterName: nameof(command));
        
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