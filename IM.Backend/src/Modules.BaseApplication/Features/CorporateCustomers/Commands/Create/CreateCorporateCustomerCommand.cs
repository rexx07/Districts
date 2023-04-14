using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.CorporateCustomers.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using Modules.BaseApplication.Services.FindeksCreditRateService;
using static Modules.BaseApplication.Features.CorporateCustomers.Constants.CorporateCustomersOperationClaims;

namespace Modules.BaseApplication.Features.CorporateCustomers.Commands.Create;

public class CreateCorporateCustomerCommand : IRequest<CreatedCorporateCustomerResponse>, ISecuredRequest
{
    public int CustomerId { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }

    public string[] Roles => new[] { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

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