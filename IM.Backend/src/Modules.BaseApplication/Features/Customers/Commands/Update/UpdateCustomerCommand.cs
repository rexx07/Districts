using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.Customers.Constants;
using Modules.BaseApplication.Features.Customers.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Customers.Constants.CustomersOperationClaims;

namespace Modules.BaseApplication.Features.Customers.Commands.Update;

public class UpdateCustomerCommand : IRequest<UpdatedCustomerResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, CustomersOperationClaims.Update };

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdatedCustomerResponse>
    {
        private readonly CustomerBusinessRules _customerBusinessRules;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IMapper mapper,
            CustomerBusinessRules customerBusinessRules
        )
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<UpdatedCustomerResponse> Handle(UpdateCustomerCommand request,
                                                          CancellationToken cancellationToken)
        {
            Customer mappedCustomer = _mapper.Map<Customer>(request);
            Customer updatedCustomer = await _customerRepository.UpdateAsync(mappedCustomer);
            UpdatedCustomerResponse updatedCustomerDto = _mapper.Map<UpdatedCustomerResponse>(updatedCustomer);
            return updatedCustomerDto;
        }
    }
}