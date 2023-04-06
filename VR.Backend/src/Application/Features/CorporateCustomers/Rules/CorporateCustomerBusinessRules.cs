using Application.Features.CorporateCustomers.Constants;
using Application.Rules;
using Domain.Entities;
using Infrastructure.Common.Exceptions.Types;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;

namespace Application.Features.CorporateCustomers.Rules;

public class CorporateCustomerBusinessRules : BaseBusinessRules
{
    private readonly ICorporateCustomerRepository _corporateCustomerRepository;

    public CorporateCustomerBusinessRules(ICorporateCustomerRepository corporateCustomerRepository)
    {
        _corporateCustomerRepository = corporateCustomerRepository;
    }

    public async Task CorporateCustomerIdShouldExistWhenSelected(int id)
    {
        CorporateCustomer? result =
            await _corporateCustomerRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(CorporateCustomersMessages.CorporateCustomerNotExists);
    }

    public Task CorporateCustomerShouldBeExist(CorporateCustomer corporateCustomer)
    {
        if (corporateCustomer is null)
            throw new BusinessException(CorporateCustomersMessages.CorporateCustomerNotExists);
        return Task.CompletedTask;
    }

    public async Task CorporateCustomerTaxNoCanNotBeDuplicatedWhenInserted(string taxNo)
    {
        IPaginate<CorporateCustomer> result = await _corporateCustomerRepository.GetListAsync(
                                                  predicate: c => c.TaxNo == taxNo,
                                                  enableTracking: false
                                              );
        if (result.Items.Any())
            throw new BusinessException(CorporateCustomersMessages.CorporateCustomerTaxNoAlreadyExists);
    }
}