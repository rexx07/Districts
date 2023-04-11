using System.Linq.Dynamic.Core;
using Application.Pipelines.Caching;
using Ardalis.GuardClauses;
using AutoMapper;
using Core.Domain.Entities.Air;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using Core.Infrastructure.Requests;
using FluentValidation;
using MediatR;
using Modules.AirTransport.Dtos;
using Modules.AirTransport.Rules;

namespace Modules.AirTransport.Commands;

public record GetAvailableFlightsQuery : IQuery<IEnumerable<FlightResponseDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public bool BypassCache => false;
    public string CacheKey => $"GetAvailableFlightsQuery({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetAvailableFlights";
    public TimeSpan? SlidingExpiration { get; }
}

public sealed class GetAvailableFlightsQueryHandler: IRequestHandler<GetAvailableFlightsQuery, IEnumerable<FlightResponseDto>>
{
    private readonly IAirRepositoryManager _airRepositoryManager;
    private readonly IMapper _mapper;
    private readonly FlightBusinessRules _flightBusinessRules;

    public GetAvailableFlightsQueryHandler(IAirRepositoryManager airRepositoryManager, IMapper mapper,
                                           FlightBusinessRules flightBusinessRules)
    {
        _airRepositoryManager = airRepositoryManager;
        _mapper = mapper;
        _flightBusinessRules = flightBusinessRules;
    }

    public async Task<IEnumerable<FlightResponseDto>> Handle(GetAvailableFlightsQuery query,
                                                             CancellationToken cancellationToken)
    {
        IPaginate<Flight> flights = await _airRepositoryManager.Flight.GetListAsync(
                                       predicate: f => !f.IsDeleted, 
                                       orderBy: f => f.OrderBy(f => f.FlightDate), 
                                       cancellationToken: cancellationToken);
        
        _flightBusinessRules.FlightsNotFound(flights);

        return _mapper.Map<IEnumerable<FlightResponseDto>>(flights);
    }
}

public class GetAvailableFlightsQueryValidator : AbstractValidator<GetAvailableFlightsQuery>
{
    GetAvailableFlightsQueryValidator(GetAvailableFlightsQuery query)
    {
        Guard.Against.Null(query, parameterName: nameof(query));
    }
}