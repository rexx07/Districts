using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Air;
using Core.Domain.Entities.BaseEntities;
using Core.Infrastructure.Persistence.Repositories;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using Core.Infrastructure.Rules;
using Modules.AirTransport.Commands;
using Modules.AirTransport.Constants;

namespace Modules.AirTransport.Rules;

public sealed class AircraftBusinessRules: BaseBusinessRules
{
    private readonly IAirRepositoryManager _repository;

    public AircraftBusinessRules(IAirRepositoryManager repository) =>
        _repository = repository;

    public async void AircraftNotExists(CreateAircraftCommand command)
    {
        Aircraft? entity = await _repository.Aircraft.GetAsync(predicate: a => a.Model == command.Model);
        if (entity is not null)
            throw new BusinessException(AirMessages.AircraftExists);
    }
}