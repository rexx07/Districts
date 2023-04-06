using Application.Features.Invoices.Constants;
using Application.Rules;
using Domain.Entities;
using Infrastructure.Common.Exceptions.Types;
using Infrastructure.Persistence.RepositoryContracts;

namespace Application.Features.Invoices.Rules;

public class InvoiceBusinessRules : BaseBusinessRules
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceBusinessRules(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task InvoiceIdShouldExistWhenSelected(int id)
    {
        Invoice? result = await _invoiceRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        if (result == null)
            throw new BusinessException(InvoicesMessages.InvoiceNotExists);
    }
}