using Application.Features.IndividualCustomers.Constants;
using Application.Rules;
using Domain.Entities;
using Infrastructure.Common.Exceptions.Types;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;

namespace Application.Features.IndividualCustomers.Rules;

public class IndividualCustomerBusinessRules : BaseBusinessRules
{
    private readonly IIndividualCustomerRepository _individualCustomerRepository;

    public IndividualCustomerBusinessRules(IIndividualCustomerRepository individualCustomerRepository)
    {
        _individualCustomerRepository = individualCustomerRepository;
    }

    public async Task IndividualCustomerIdShouldExistWhenSelected(int id)
    {
        IndividualCustomer? result =
            await _individualCustomerRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(IndividualCustomersMessages.IndividualCustomerNotExists);
    }

    public Task IndividualCustomerShouldBeExist(IndividualCustomer? individualCustomer)
    {
        if (individualCustomer is null)
            throw new BusinessException(IndividualCustomersMessages.IndividualCustomerNotExists);
        return Task.CompletedTask;
    }

    public async Task IndividualCustomerNationalIdentityCanNotBeDuplicatedWhenInserted(string nationalIdentity)
    {
        IPaginate<IndividualCustomer> result = await _individualCustomerRepository.GetListAsync(
                                                   c => c.NationalIdentity == nationalIdentity
                                               );
        if (result.Items.Any())
            throw new BusinessException(IndividualCustomersMessages.IndividualCustomerNationalIdentityAlreadyExists);
    }
}