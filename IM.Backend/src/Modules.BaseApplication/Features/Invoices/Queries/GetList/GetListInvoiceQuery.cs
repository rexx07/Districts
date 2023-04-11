using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using MediatR;

namespace Application.Features.Invoices.Queries.GetList;

public class GetListInvoiceQuery : IRequest<GetListResponse<GetListInvoiceListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListInvoiceQueryHandler : IRequestHandler<GetListInvoiceQuery, GetListResponse<GetListInvoiceListItemDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public GetListInvoiceQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListInvoiceListItemDto>> Handle(
            GetListInvoiceQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Invoice> invoices = await _invoiceRepository.GetListAsync(
                                              index: request.PageRequest.Page,
                                              size: request.PageRequest.PageSize
                                          );
            var mappedInvoiceListModel = _mapper.Map<GetListResponse<GetListInvoiceListItemDto>>(invoices);
            return mappedInvoiceListModel;
        }
    }
}