using Application.Features.Invoices.Constants;
using Application.Features.Invoices.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using static Application.Features.Invoices.Constants.InvoicesOperationClaims;

namespace Application.Features.Invoices.Commands.Update;

public class UpdateInvoiceCommand : IRequest<UpdatedInvoiceResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string No { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public short TotalRentalDate { get; set; }
    public decimal RentalPrice { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, InvoicesOperationClaims.Update };

    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, UpdatedInvoiceResponse>
    {
        private readonly InvoiceBusinessRules _invoiceBusinessRules;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public UpdateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMapper mapper,
                                           InvoiceBusinessRules invoiceBusinessRules)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _invoiceBusinessRules = invoiceBusinessRules;
        }

        public async Task<UpdatedInvoiceResponse> Handle(UpdateInvoiceCommand request,
                                                         CancellationToken cancellationToken)
        {
            Invoice mappedInvoice = _mapper.Map<Invoice>(request);
            Invoice updatedInvoice = await _invoiceRepository.UpdateAsync(mappedInvoice);
            UpdatedInvoiceResponse updatedInvoiceDto = _mapper.Map<UpdatedInvoiceResponse>(updatedInvoice);
            return updatedInvoiceDto;
        }
    }
}