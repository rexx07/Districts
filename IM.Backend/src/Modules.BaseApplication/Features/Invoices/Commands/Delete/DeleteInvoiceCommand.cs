using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.Invoices.Constants;
using Modules.BaseApplication.Features.Invoices.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Invoices.Constants.InvoicesOperationClaims;

namespace Modules.BaseApplication.Features.Invoices.Commands.Delete;

public class DeleteInvoiceCommand : IRequest<DeletedInvoiceResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, InvoicesOperationClaims.Delete };

    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, DeletedInvoiceResponse>
    {
        private readonly InvoiceBusinessRules _invoiceBusinessRules;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public DeleteInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMapper mapper,
                                           InvoiceBusinessRules invoiceBusinessRules)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _invoiceBusinessRules = invoiceBusinessRules;
        }

        public async Task<DeletedInvoiceResponse> Handle(DeleteInvoiceCommand request,
                                                         CancellationToken cancellationToken)
        {
            await _invoiceBusinessRules.InvoiceIdShouldExistWhenSelected(request.Id);

            Invoice mappedInvoice = _mapper.Map<Invoice>(request);
            Invoice deletedInvoice = await _invoiceRepository.DeleteAsync(mappedInvoice);
            DeletedInvoiceResponse deletedInvoiceDto = _mapper.Map<DeletedInvoiceResponse>(deletedInvoice);
            return deletedInvoiceDto;
        }
    }
}