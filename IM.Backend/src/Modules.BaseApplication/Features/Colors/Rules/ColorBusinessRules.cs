using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Rules;
using Modules.BaseApplication.Features.Colors.Constants;

namespace Modules.BaseApplication.Features.Colors.Rules;

public class ColorBusinessRules : BaseBusinessRules
{
    private readonly IColorRepository _colorRepository;

    public ColorBusinessRules(IColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }

    public async Task ColorIdShouldExistWhenSelected(int id)
    {
        Color? result = await _colorRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(ColorsMessages.ColorNotExists);
    }

    public async Task ColorNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Color> result =
            await _colorRepository.GetListAsync(predicate: b => b.Name == name, enableTracking: false);
        if (result.Items.Any())
            throw new BusinessException(ColorsMessages.ColorNameExists);
    }
}