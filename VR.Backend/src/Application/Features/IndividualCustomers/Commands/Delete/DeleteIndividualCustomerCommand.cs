using Application.Features.IndividualCustomers.Constants;
using Application.Features.IndividualCustomers.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.IndividualCustomers.Constants.IndividualCustomersOperationClaims;

namespace Application.Features.IndividualCustomers.Commands.Delete;

public class DeleteIndividualCustomerCommand : IRequest<DeletedIndividualCustomerResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Domain.Constants.OperationClaims.Admin, Admin, Write, IndividualCustomersOperationClaims.Delete };

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