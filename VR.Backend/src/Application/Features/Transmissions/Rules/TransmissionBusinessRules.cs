using Application.Features.Transmissions.Constants;
using Application.Rules;
using Domain.Entities;
using Infrastructure.Common.Exceptions.Types;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;

namespace Application.Features.Transmissions.Rules;

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