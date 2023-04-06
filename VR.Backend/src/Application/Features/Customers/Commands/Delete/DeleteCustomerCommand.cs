using Application.Features.Customers.Constants;
using Application.Features.Customers.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.Customers.Constants.CustomersOperationClaims;

namespace Application.Features.Customers.Commands.Delete;

public class DeleteCustomerCommand : IRequest<DeletedCustomerResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Domain.Constants.OperationClaims.Admin, Admin, Write, CustomersOperationClaims.Delete };

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, DeletedCustomerResponse>
    {
        private readonly CustomerBusinessRules _customerBusinessRules;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public DeleteCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IMapper mapper,
            CustomerBusinessRules customerBusinessRules
        )
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<DeletedCustomerResponse> Handle(DeleteCustomerCommand request,
                                                          CancellationToken cancellationToken)
        {
            await _customerBusinessRules.CustomerIdShouldExist(request.Id);

            Customer mappedCustomer = _mapper.Map<Customer>(request);
            Customer deletedCustomer = await _customerRepository.DeleteAsync(mappedCustomer);
            DeletedCustomerResponse deletedCustomerDto = _mapper.Map<DeletedCustomerResponse>(deletedCustomer);
            return deletedCustomerDto;
        }
    }
}