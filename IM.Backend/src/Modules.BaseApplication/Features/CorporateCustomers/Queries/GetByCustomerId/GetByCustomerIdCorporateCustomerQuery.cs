using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.CorporateCustomers.Rules;

namespace Modules.BaseApplication.Features.CorporateCustomers.Queries.GetByCustomerId;

public class GetByCustomerIdCorporateCustomerQuery : IRequest<GetByCustomerIdCorporateCustomerResponse>
{
    public int CustomerId { get; set; }

    public class GetByIdCorporateCustomerQueryHandler
        : IRequestHandler<GetByCustomerIdCorporateCustomerQuery, GetByCustomerIdCorporateCustomerResponse>
    {
        private readonly CorporateCustomerBusinessRules _corporateCustomerBusinessRules;
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly IMapper _mapper;

        public GetByIdCorporateCustomerQueryHandler(
            ICorporateCustomerRepository corporateCustomerRepository,
            CorporateCustomerBusinessRules corporateCustomerBusinessRules,
            IMapper mapper
        )
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByCustomerIdCorporateCustomerResponse> Handle(
            GetByCustomerIdCorporateCustomerQuery request,
            CancellationToken cancellationToken
        )
        {
            CorporateCustomer? corporateCustomer =
                await _corporateCustomerRepository.GetAsync(b => b.CustomerId == request.CustomerId);
            await _corporateCustomerBusinessRules.CorporateCustomerShouldBeExist(corporateCustomer);
            GetByCustomerIdCorporateCustomerResponse corporateCustomerDto =
                _mapper.Map<GetByCustomerIdCorporateCustomerResponse>(
                    corporateCustomer
                );
            return corporateCustomerDto;
        }
    }
}