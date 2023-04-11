using Application.Features.Models.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Rules;

namespace Application.Features.Models.Rules;

public class ModelBusinessRules : BaseBusinessRules
{
    private readonly IModelRepository _modelRepository;

    public ModelBusinessRules(IModelRepository modelRepository)
    {
        _modelRepository = modelRepository;
    }

    public async Task ModelIdShouldExistWhenSelected(int id)
    {
        Model? result = await _modelRepository.GetAsync(predicate: c => c.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(ModelsMessages.ModelNotExists);
    }
}