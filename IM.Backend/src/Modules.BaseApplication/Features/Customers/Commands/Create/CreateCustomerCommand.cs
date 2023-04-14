using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.Customers.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Customers.Constants.CustomersOperationClaims;

namespace Modules.BaseApplication.Features.Customers.Commands.Create;

public class CreateCustomerCommand : IRequest<CreatedCustomerResponse>, ISecuredRequest
{
    public int UserId { get; set; }

    public string[] Roles => new[] { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreatedCustomerResponse>
    {
        private readonly CustomerBusinessRules _customerBusinessRules;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IMapper mapper,
            CustomerBusinessRules customerBusinessRules
        )
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<CreatedCustomerResponse> Handle(CreateCustomerCommand request,
                                                          CancellationToken cancellationToken)
        {
            Customer mappedCustomer = _mapper.Map<Customer>(request);
            Customer createdCustomer = await _customerRepository.AddAsync(mappedCustomer);
            CreatedCustomerResponse createdCustomerDto = _mapper.Map<CreatedCustomerResponse>(createdCustomer);
            return createdCustomerDto;
        }
    }
}