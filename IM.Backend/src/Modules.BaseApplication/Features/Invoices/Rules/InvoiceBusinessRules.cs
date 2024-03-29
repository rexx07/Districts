using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using Core.Infrastructure.Rules;
using Modules.BaseApplication.Features.Invoices.Constants;

namespace Modules.BaseApplication.Features.Invoices.Rules;

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