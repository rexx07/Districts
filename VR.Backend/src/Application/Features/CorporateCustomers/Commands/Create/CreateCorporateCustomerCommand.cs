using Application.Features.CorporateCustomers.Rules;
using Application.Pipelines.Authorization;
using Application.Services.FindeksCreditRateService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.CorporateCustomers.Constants.CorporateCustomersOperationClaims;

namespace Application.Features.CorporateCustomers.Commands.Create;

public class CreateCorporateCustomerCommand : IRequest<CreatedCorporateCustomerResponse>, ISecuredRequest
{
    public int CustomerId { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class
        CreateCorporateCustomerCommandHandler : IRequestHandler<CreateCorporateCustomerCommand,
            CreatedCorporateCustomerResponse>
    {
        private readonly CorporateCustomerBusinessRules _corporateCustomerBusinessRules;
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly IFindeksCreditRateService _findeksCreditRateService;
        private readonly IMapper _mapper;

        public CreateCorporateCustomerCommandHandler(
            ICorporateCustomerRepository corporateCustomerRepository,
            IMapper mapper,
            CorporateCustomerBusinessRules corporateCustomerBusinessRules,
            IFindeksCreditRateService findeksCreditRateService
        )
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _mapper = mapper;
            _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
            _findeksCreditRateService = findeksCreditRateService;
        }

        public async Task<CreatedCorporateCustomerResponse> Handle(
            CreateCorporateCustomerCommand request,
            CancellationToken cancellationToken
        )
        {
            await _corporateCustomerBusinessRules.CorporateCustomerTaxNoCanNotBeDuplicatedWhenInserted(request.TaxNo);

            CorporateCustomer mappedCorporateCustomer = _mapper.Map<CorporateCustomer>(request);
            CorporateCustomer createdCorporateCustomer =
                await _corporateCustomerRepository.AddAsync(mappedCorporateCustomer);

            await _findeksCreditRateService.Add(new FindeksCreditRate
                                                    { CustomerId = createdCorporateCustomer.CustomerId });

            CreatedCorporateCustomerResponse createdCorporateCustomerDto =
                _mapper.Map<CreatedCorporateCustomerResponse>(
                    createdCorporateCustomer
                );
            return createdCorporateCustomerDto;
        }
    }
}