using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Air;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using Core.Infrastructure.Rules;
using Modules.AirTransport.Commands;
using Modules.AirTransport.Constants;

namespace Modules.AirTransport.Rules;

public sealed class AirportBusinessRules: BaseBusinessRules
{
    private readonly IAirRepositoryManager _repository;

    public AirportBusinessRules(IAirRepositoryManager repository) =>
        _repository = repository;

    public async void AirportNotExists(CreateAirportCommand command)
    {
        Airport? entity = await _repository.Airport.GetAsync(predicate: a => a.Code == command.Code);
        if (entity is not null)
            throw new BusinessException(AirMessages.AirportExists);
    }
}