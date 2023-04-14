using Ardalis.GuardClauses;
using AutoMapper;
using Core.Domain.Entities.PassengerAndCargo;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.RepositoryContracts.PassengerAndCargo;
using FluentValidation;
using Modules.PassengerAndCargo.Dtos;
using Modules.PassengerAndCargo.Rules;

namespace Modules.PassengerAndCargo.Queries;

public sealed record GetPassengerQueryById(long Id) : IQuery<PassengerResponseDto>;

public sealed class GetPassengerByIdQueryHandler : IQueryHandler<GetPassengerQueryById, PassengerResponseDto>
{
    private readonly IPassengerAndCargoRepositoryManager _passengerAndCargo;
    private readonly IMapper _mapper;
    private PassengerBusinessRules _passengerBusinessRules;

    public GetPassengerByIdQueryHandler(IPassengerAndCargoRepositoryManager passengerAndCargo, IMapper mapper,
                                        PassengerBusinessRules passengerBusinessRules)
    {
        _passengerAndCargo = passengerAndCargo;
        _mapper = mapper;
        passengerBusinessRules = passengerBusinessRules;
    }
    
    public async Task<PassengerResponseDto> Handle(GetPassengerQueryById query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query);
        Passenger? passenger = await _passengerAndCargo.Passenger.GetAsync(p => p.Id == query.Id,
                                                                           cancellationToken: cancellationToken);
        _passengerBusinessRules.PassengerExists(passenger);

        return _mapper.Map<PassengerResponseDto>(passenger);
    }
}

public sealed class GetPassengerQueryByIdValidator: AbstractValidator<GetPassengerQueryById>
{
    public GetPassengerQueryByIdValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id).NotNull().WithMessage("Id is required!");
    }
}