using Ardalis.GuardClauses;
using AutoMapper;
using Core.CrossCuttingConcerns.IdsGenerator;
using Core.Domain.Entities.PassengerAndCargo;
using Core.Domain.Enums;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.RepositoryContracts.PassengerAndCargo;
using FluentValidation;
using Modules.PassengerAndCargo.Dtos;
using Modules.PassengerAndCargo.Rules;

namespace Modules.PassengerAndCargo.Commands;

public sealed record CompleteRegisterPassengerCommand(string PassportNumber, PassengerType PassengerType, int Age) : ICommand<PassengerResponseDto>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}

public sealed class CompleteRegisterPassengerCommandHandler : ICommandHandler<CompleteRegisterPassengerCommand,
    PassengerResponseDto>
{
    private readonly IPassengerAndCargoRepositoryManager _passengerAndCargo;
    private readonly IMapper _mapper;
    private readonly PassengerBusinessRules _passengerBusinessRules;

    public CompleteRegisterPassengerCommandHandler(IPassengerAndCargoRepositoryManager passengerAndCargo,
                                                   IMapper mapper,
                                                   PassengerBusinessRules passengerBusinessRules)
    {
        _passengerAndCargo = passengerAndCargo;
        _mapper = mapper;
        _passengerBusinessRules = passengerBusinessRules;
    }

    public async Task<PassengerResponseDto> Handle(CompleteRegisterPassengerCommand command,
                                                   CancellationToken cancellationToken)
    {
        Guard.Against.Null(command);
        Passenger? passenger = await _passengerAndCargo.Passenger.GetAsync(predicate: p => p.PassportNumber
                                                                         == command.PassportNumber,
                                                                        cancellationToken: cancellationToken);
        _passengerBusinessRules.PassengerExists(passenger);

        Passenger passengerEntity = passenger.CompleteRegistrationPassenger(
            passenger.Id, passenger.Name, passenger.PassportNumber,
            command.PassengerType, command.Age);

        Passenger passengerUpdate = await _passengerAndCargo.Passenger.UpdateAsync(passengerEntity, cancellationToken);

        return _mapper.Map<PassengerResponseDto>(passengerUpdate);
    }
}

public class CompleteRegisterPassengerCommandValidator : AbstractValidator<CompleteRegisterPassengerCommand>
{
    public CompleteRegisterPassengerCommandValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.PassportNumber).NotNull().WithMessage("The PassportNumber is required!");
        RuleFor(x => x.Age).GreaterThan(0).WithMessage("The Age must be greater than 0!");
        RuleFor(x => x.PassengerType).Must(p => p.GetType().IsEnum &&
                                                p == PassengerType.Baby ||
                                                p == PassengerType.Female ||
                                                p == PassengerType.Male ||
                                                p == PassengerType.Unknown)
                                     .WithMessage("PassengerType must be Male, Female, Baby or Unknown");
    }
}
 