using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Air;
using Core.Infrastructure.CQRS;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using Core.Infrastructure.Rules;
using Modules.AirTransport.Commands;
using Modules.AirTransport.Constants;
using OneOf;

namespace Modules.AirTransport.Rules;

public sealed class SeatBusinessRules: BaseBusinessRules
{
    private readonly IAirRepositoryManager _repository;
    
    public SeatBusinessRules(IAirRepositoryManager repository) =>
        _repository = repository;

    internal async void SeatNotExists(CreateSeatCommand seat)
    {
        Seat? entity = await _repository.Seat.GetAsync(predicate: s => s.Id == seat.Id);
        if (entity is not null)
            throw new BusinessException(AirMessages.SeatExists);
    }

    internal void SeatExists(Seat seat)
    {
        if (seat is null)
            throw new BusinessException(AirMessages.SeatNumberIncorrect);
    }
}