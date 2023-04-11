using Ardalis.GuardClauses;
using AutoMapper;
using Core.Domain.Entities.Air;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using MediatR;
using Modules.AirTransport.Dtos;

namespace Modules.AirTransport.Commands;

public record GetAvailableSeatsQuery(long FlightId) : IQuery<IEnumerable<SeatResponseDto>>;

public class GetAvailableSeatsQueryHandler : IRequestHandler<GetAvailableSeatsQuery, IEnumerable<SeatResponseDto>>
{
    private readonly IAirRepositoryManager _airRepositoryManager;
    private readonly IMapper _mapper;

    public GetAvailableSeatsQueryHandler(IAirRepositoryManager airRepositoryManager, IMapper mapper)
    {
        _airRepositoryManager = airRepositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SeatResponseDto>> Handle(GetAvailableSeatsQuery query,
                                                           CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, parameterName: nameof(query));

        IPaginate<Seat> seats = await _airRepositoryManager.Seat.GetListAsync(
                                    predicate: s => s.FlightId == query.FlightId && !s.IsDeleted,
                                    orderBy: s => s.OrderBy(s => s.SeatNumber),
                                    cancellationToken: cancellationToken);
    }
}