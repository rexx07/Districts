using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using MediatR;

namespace Modules.BaseApplication.Features.Customers.Queries.GetList;

public class GetListCustomerQuery : IRequest<GetListResponse<GetListCustomerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListCustomerQueryHandler : IRequestHandler<GetListCustomerQuery, GetListResponse<GetListCustomerListItemDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetListCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCustomerListItemDto>> Handle(
            GetListCustomerQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Customer> customers = await _customerRepository.GetListAsync(
                                                index: request.PageRequest.Page,
                                                size: request.PageRequest.PageSize
                                            );
            var mappedCustomerListModel = _mapper.Map<GetListResponse<GetListCustomerListItemDto>>(customers);
            return mappedCustomerListModel;
        }
    }
}