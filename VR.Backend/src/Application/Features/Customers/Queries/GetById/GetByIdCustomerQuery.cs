using Application.Features.Customers.Rules;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;

namespace Application.Features.Customers.Queries.GetById;

public class GetByIdCustomerQuery : IRequest<GetByIdCustomerResponse>
{
    public int Id { get; set; }

    public class GetByIdCustomerQueryHandler : IRequestHandler<GetByIdCustomerQuery, GetByIdCustomerResponse>
    {
        private readonly CustomerBusinessRules _customerBusinessRules;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetByIdCustomerQueryHandler(
            ICustomerRepository customerRepository,
            CustomerBusinessRules customerBusinessRules,
            IMapper mapper
        )
        {
            _customerRepository = customerRepository;
            _customerBusinessRules = customerBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByIdCustomerResponse> Handle(GetByIdCustomerQuery request,
                                                          CancellationToken cancellationToken)
        {
            Customer? customer = await _customerRepository.GetAsync(b => b.Id == request.Id);
            await _customerBusinessRules.CustomerShouldBeExist(customer);
            GetByIdCustomerResponse customerDto = _mapper.Map<GetByIdCustomerResponse>(customer);
            return customerDto;
        }
    }
}