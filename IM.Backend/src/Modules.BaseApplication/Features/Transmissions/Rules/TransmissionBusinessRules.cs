using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Rules;
using Modules.BaseApplication.Features.Transmissions.Constants;

namespace Modules.BaseApplication.Features.Transmissions.Rules;

public class TransmissionBusinessRules : BaseBusinessRules
{
    private readonly ITransmissionRepository _transmissionRepository;

    public TransmissionBusinessRules(ITransmissionRepository transmissionRepository)
    {
        _transmissionRepository = transmissionRepository;
    }

    public async Task TransmissionIdShouldExistWhenSelected(int id)
    {
        Transmission? result =
            await _transmissionRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(TransmissionsMessages.TransmissionNotExists);
    }

    public async Task TransmissionNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Transmission> result =
            await _transmissionRepository.GetListAsync(predicate: b => b.Name == name, enableTracking: false);
        if (result.Items.Any())
            throw new BusinessException(TransmissionsMessages.TransmissionNameExists);
    }
}