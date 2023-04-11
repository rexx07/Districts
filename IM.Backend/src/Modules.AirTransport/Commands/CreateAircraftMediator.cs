using Ardalis.GuardClauses;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.CrossCuttingConcerns.IdsGenerator;
using Core.Domain.Entities.Air;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using FluentValidation;
using MediatR;
using Modules.AirTransport.Constants;
using Modules.AirTransport.Dtos;
using Modules.AirTransport.Rules;

namespace Modules.AirTransport.Commands;

public sealed record CreateAircraftCommand(string Name, string Model, int ManufacturingYear) : ICommand<AircraftResponseDto>
{
    public long Id { get;} = SnowFlakIdGenerator.NewId();
}

public sealed class CreateAircraftHandler : IRequestHandler<CreateAircraftCommand, AircraftResponseDto>
{
    private readonly IAirRepositoryManager _aircraftRepositoryManager;
    private readonly AircraftBusinessRules _aircraftBusinessRules;
    private readonly IMapper _mapper;

    public CreateAircraftHandler(IAirRepositoryManager airRepositoryManager, IMapper mapper,
        AircraftBusinessRules aircraftBusinessRules)
    {
        _mapper = mapper;
        _aircraftRepositoryManager = airRepositoryManager;
        _aircraftBusinessRules = aircraftBusinessRules;
    }


    public async Task<AircraftResponseDto> Handle(CreateAircraftCommand command, CancellationToken cancellationToken)
    {
        _aircraftBusinessRules.AircraftNotExists(command);
        
        Aircraft aircraftEntity = Aircraft.Create(command.Id, command.Name, command.Model, command.ManufacturingYear);
        Aircraft newAircraft = await _aircraftRepositoryManager.Aircraft.AddAsync(aircraftEntity, cancellationToken);

        return _mapper.Map<AircraftResponseDto>(newAircraft);
    }
}

public sealed class CreateAircraftCommandValidator: AbstractValidator<CreateAircraftCommand>
{
    public CreateAircraftCommandValidator(CreateAircraftCommand command)
    {
        Guard.Against.Null(command, parameterName: nameof(command));
        
        RuleFor(x => x.Model).NotEmpty().WithMessage("Model is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.ManufacturingYear).NotEmpty().WithMessage("ManufacturingYear is required");
    }
}