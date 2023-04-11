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

public sealed record GetFlightByIdQuery(long Id) : IQuery<FlightResponseDto>;

public sealed class GetFlightByIdQueryHandler : IRequestHandler<GetFlightByIdQuery, FlightResponseDto>
{
    private readonly IAirRepositoryManager _airRepositoryManager;
    private readonly IMapper _mapper;
    private readonly FlightBusinessRules _flightBusinessRules;

    public GetFlightByIdQueryHandler(IAirRepositoryManager airRepositoryManager, IMapper mapper,
                                           FlightBusinessRules flightBusinessRules)
    {
        _airRepositoryManager = airRepositoryManager;
        _mapper = mapper;
        _flightBusinessRules = flightBusinessRules;
    }
    
    public async Task<FlightResponseDto> Handle(GetFlightByIdQuery query, CancellationToken cancellationToken)
    {
        Flight? flight = await _airRepositoryManager.Flight.GetAsync(predicate: f => f.Id == query.Id, 
                                                                     cancellationToken: cancellationToken);
        
        _flightBusinessRules.FlightExists(flight);

        return _mapper.Map<FlightResponseDto>(flight);
    }
}

public class GetFlightByIdQueryValidator : AbstractValidator<GetFlightByIdQuery>
{
    public GetFlightByIdQueryValidator(GetFlightByIdQuery query)
    {
        Guard.Against.Null(query, parameterName: nameof(query));
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id).NotNull().WithMessage("Id is required!");
    }
}