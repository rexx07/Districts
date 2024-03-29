using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.IndividualCustomers.Constants;
using Modules.BaseApplication.Features.IndividualCustomers.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.IndividualCustomers.Constants.IndividualCustomersOperationClaims;

namespace Modules.BaseApplication.Features.IndividualCustomers.Commands.Delete;

public class DeleteIndividualCustomerCommand : IRequest<DeletedIndividualCustomerResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, IndividualCustomersOperationClaims.Delete };

    public class DeleteIndividualCustomerCommandHandler
        : IRequestHandler<DeleteIndividualCustomerCommand, DeletedIndividualCustomerResponse>
    {
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;

        public DeleteIndividualCustomerCommandHandler(
            IIndividualCustomerRepository individualCustomerRepository,
            IMapper mapper,
            IndividualCustomerBusinessRules individualCustomerBusinessRules
        )
        {
            _individualCustomerRepository = individualCustomerRepository;
            _mapper = mapper;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
        }

        public async Task<DeletedIndividualCustomerResponse> Handle(
            DeleteIndividualCustomerCommand request,
            CancellationToken cancellationToken
        )
        {
            await _individualCustomerBusinessRules.IndividualCustomerIdShouldExistWhenSelected(request.Id);

            IndividualCustomer mappedIndividualCustomer = _mapper.Map<IndividualCustomer>(request);
            IndividualCustomer deletedIndividualCustomer =
                await _individualCustomerRepository.DeleteAsync(mappedIndividualCustomer);
            DeletedIndividualCustomerResponse deletedIndividualCustomerDto =
                _mapper.Map<DeletedIndividualCustomerResponse>(
                    deletedIndividualCustomer
                );
            return deletedIndividualCustomerDto;
        }
    }
}