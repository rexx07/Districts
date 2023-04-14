using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.IndividualCustomers.Constants;
using Modules.BaseApplication.Features.IndividualCustomers.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.IndividualCustomers.Constants.IndividualCustomersOperationClaims;

namespace Modules.BaseApplication.Features.IndividualCustomers.Commands.Update;

public class UpdateIndividualCustomerCommand : IRequest<UpdatedIndividualCustomerResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, IndividualCustomersOperationClaims.Update };

    public class UpdateIndividualCustomerCommandHandler
        : IRequestHandler<UpdateIndividualCustomerCommand, UpdatedIndividualCustomerResponse>
    {
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;

        public UpdateIndividualCustomerCommandHandler(
            IIndividualCustomerRepository individualCustomerRepository,
            IMapper mapper,
            IndividualCustomerBusinessRules individualCustomerBusinessRules
        )
        {
            _individualCustomerRepository = individualCustomerRepository;
            _mapper = mapper;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
        }

        public async Task<UpdatedIndividualCustomerResponse> Handle(
            UpdateIndividualCustomerCommand request,
            CancellationToken cancellationToken
        )
        {
            await _individualCustomerBusinessRules.IndividualCustomerNationalIdentityCanNotBeDuplicatedWhenInserted(
                request.NationalIdentity
            );

            IndividualCustomer mappedIndividualCustomer = _mapper.Map<IndividualCustomer>(request);
            IndividualCustomer updatedIndividualCustomer =
                await _individualCustomerRepository.UpdateAsync(mappedIndividualCustomer);
            UpdatedIndividualCustomerResponse updatedIndividualCustomerDto =
                _mapper.Map<UpdatedIndividualCustomerResponse>(
                    updatedIndividualCustomer
                );
            return updatedIndividualCustomerDto;
        }
    }
}