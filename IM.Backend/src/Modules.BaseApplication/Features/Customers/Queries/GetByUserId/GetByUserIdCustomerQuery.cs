using Application.Features.Customers.Rules;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.Customers.Queries.GetByUserId;

public class GetByUserIdCustomerQuery : IRequest<GetByUserIdCustomerResponse>
{
    public int UserId { get; set; }

    public class
        GetByUserIdCustomerQueryHandler : IRequestHandler<GetByUserIdCustomerQuery, GetByUserIdCustomerResponse>
    {
        private readonly CustomerBusinessRules _businessRules;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetByUserIdCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper,
                                               CustomerBusinessRules businessRules)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<GetByUserIdCustomerResponse> Handle(GetByUserIdCustomerQuery request,
                                                              CancellationToken cancellationToken)
        {
            Customer? customer = await _customerRepository.GetAsync(c => c.UserId == request.UserId);
            await _businessRules.CustomerShouldBeExist(customer);
            GetByUserIdCustomerResponse? customerDto = _mapper.Map<GetByUserIdCustomerResponse>(customer);
            return customerDto;
        }
    }
}