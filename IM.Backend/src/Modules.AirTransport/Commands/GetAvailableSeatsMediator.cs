using Ardalis.GuardClauses;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Air;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using FluentValidation;
using MediatR;
using Modules.AirTransport.Constants;
using Modules.AirTransport.Dtos;

namespace Modules.AirTransport.Commands;

public sealed record GetAvailableSeatsQuery(long FlightId) : IQuery<IEnumerable<SeatResponseDto>>;

public sealed class GetAvailableSeatsQueryHandler : IRequestHandler<GetAvailableSeatsQuery, IEnumerable<SeatResponseDto>>
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
        IPaginate<Seat> seats = await _airRepositoryManager.Seat.GetListAsync(
                                    predicate: s => s.FlightId == query.FlightId && !s.IsDeleted,
                                    orderBy: s => s.OrderBy(s => s.SeatNumber),
                                    cancellationToken: cancellationToken);

        if (seats.Count == 0)
            throw new BusinessException(AirMessages.AllSeatsFullException);

        return _mapper.Map<IEnumerable<SeatResponseDto>>(seats);
    }
}

public sealed class GetAvailableSeatsQueryValidator: AbstractValidator<GetAvailableSeatsQuery>
{
    public GetAvailableSeatsQueryValidator(GetAvailableSeatsQuery query)
    {
        Guard.Against.Null(query, parameterName: nameof(query));
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.FlightId).NotNull().WithMessage("FlightId is required!");
    }
}