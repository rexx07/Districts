using Ardalis.GuardClauses;
using AutoMapper;
using Core.CrossCuttingConcerns.IdsGenerator;
using Core.Domain.Entities.Air;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using FluentValidation;
using MediatR;
using Modules.AirTransport.Dtos;
using Modules.AirTransport.Rules;

namespace Modules.AirTransport.Commands;

public sealed record CreateAirportCommand(string Name, string Address, string Code): ICommand<AirportResponseDto>
{
    public long Id { get; } = SnowFlakIdGenerator.NewId();
}

public sealed class CreateAirportHandler : ICommandHandler<CreateAirportCommand, AirportResponseDto>
{
    private readonly IAirRepositoryManager _airRepository;
    private readonly IMapper _mapper;
    private readonly AirportBusinessRules _airportBusinessRules;

    public CreateAirportHandler(IAirRepositoryManager airRepositoryManager, IMapper mapper, 
                                AirportBusinessRules airportBusinessRules)
    {
        _airRepository = airRepositoryManager;
        _mapper = mapper;
        _airportBusinessRules = airportBusinessRules;
    }

    public async Task<AirportResponseDto> Handle(CreateAirportCommand command, CancellationToken cancellationToken)
    {
        _airportBusinessRules.AirportNotExists(command);

        Airport airportEntity = Airport.Create(command.Id, command.Name, command.Code, command.Address);
        Airport newAirport = await _airRepository.Airport.AddAsync(airportEntity, cancellationToken);

        return _mapper.Map<AirportResponseDto>(newAirport);
    }

    public sealed class CreateAirportCommandValidator : AbstractValidator<CreateAirportCommand>
    {
        public CreateAirportCommandValidator(CreateAirportCommand command)
        {
            Guard.Against.Null(command, parameterName: nameof(command));
            
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
        }
    }
}