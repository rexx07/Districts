using Application.Features.Invoices.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using static Application.Features.Invoices.Constants.InvoicesOperationClaims;

namespace Application.Features.Invoices.Commands.Create;

public class CreateInvoiceCommand : IRequest<CreatedInvoiceResponse>, ISecuredRequest
{
    public int CustomerId { get; set; }
    public string No { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public short TotalRentalDate { get; set; }
    public decimal RentalPrice { get; set; }

    public string[] Roles => new[] { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, CreatedInvoiceResponse>
    {
        private readonly InvoiceBusinessRules _invoiceBusinessRules;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMapper mapper,
                                           InvoiceBusinessRules invoiceBusinessRules)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _invoiceBusinessRules = invoiceBusinessRules;
        }

        public async Task<CreatedInvoiceResponse> Handle(CreateInvoiceCommand request,
                                                         CancellationToken cancellationToken)
        {
            Invoice mappedInvoice = _mapper.Map<Invoice>(request);
            Invoice createdInvoice = await _invoiceRepository.AddAsync(mappedInvoice);
            CreatedInvoiceResponse createdInvoiceDto = _mapper.Map<CreatedInvoiceResponse>(createdInvoice);
            return createdInvoiceDto;
        }
    }
}