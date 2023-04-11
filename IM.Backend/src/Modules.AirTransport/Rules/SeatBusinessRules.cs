using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Air;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using Core.Infrastructure.Rules;
using Modules.AirTransport.Commands;
using Modules.AirTransport.Constants;

namespace Modules.AirTransport.Rules;

public sealed class SeatBusinessRules: BaseBusinessRules
{
    private readonly IAirRepositoryManager _repository;

    public SeatBusinessRules(IAirRepositoryManager repository) =>
        _repository = repository;

    async void SeatExists(CreateSeatCommand command)
    {
        Seat? entity = await _repository.Seat.GetAsync(predicate: s => s.Id == command.Id);
        if (entity is not null)
            throw new BusinessException(AirMessages.SeatExists);
    }

    public async void Check(CreateSeatCommand command)
    {
        SeatExists(command);
    }
}