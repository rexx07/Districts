using Application.Features.IndividualCustomers.Rules;
using Application.Pipelines.Authorization;
using Application.Services.FindeksCreditRateService;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using static Application.Features.IndividualCustomers.Constants.IndividualCustomersOperationClaims;

namespace Application.Features.IndividualCustomers.Commands.Create;

public class CreateIndividualCustomerCommand : IRequest<CreatedIndividualCustomerResponse>, ISecuredRequest
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }

    public string[] Roles => new[] { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class CreateIndividualCustomerCommandHandler
        : IRequestHandler<CreateIndividualCustomerCommand, CreatedIndividualCustomerResponse>
    {
        private readonly IFindeksCreditRateService _findeksCreditRateService;
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;

        public CreateIndividualCustomerCommandHandler(
            IIndividualCustomerRepository individualCustomerRepository,
            IMapper mapper,
            IndividualCustomerBusinessRules individualCustomerBusinessRules,
            IFindeksCreditRateService findeksCreditRateService
        )
        {
            _individualCustomerRepository = individualCustomerRepository;
            _mapper = mapper;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
            _findeksCreditRateService = findeksCreditRateService;
        }

        public async Task<CreatedIndividualCustomerResponse> Handle(
            CreateIndividualCustomerCommand request,
            CancellationToken cancellationToken
        )
        {
            await _individualCustomerBusinessRules.IndividualCustomerNationalIdentityCanNotBeDuplicatedWhenInserted(
                request.NationalIdentity
            );

            IndividualCustomer mappedIndividualCustomer = _mapper.Map<IndividualCustomer>(request);
            IndividualCustomer createdIndividualCustomer =
                await _individualCustomerRepository.AddAsync(mappedIndividualCustomer);

            await _findeksCreditRateService.Add(new FindeksCreditRate
                                                    { CustomerId = createdIndividualCustomer.CustomerId });

            CreatedIndividualCustomerResponse createdIndividualCustomerDto =
                _mapper.Map<CreatedIndividualCustomerResponse>(
                    createdIndividualCustomer
                );
            return createdIndividualCustomerDto;
        }
    }
}