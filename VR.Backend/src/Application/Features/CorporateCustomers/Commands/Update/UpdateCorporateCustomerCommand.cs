using Application.Features.CorporateCustomers.Constants;
using Application.Features.CorporateCustomers.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.CorporateCustomers.Constants.CorporateCustomersOperationClaims;

namespace Application.Features.CorporateCustomers.Commands.Update;

public class UpdateCorporateCustomerCommand : IRequest<UpdatedCorporateCustomerResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }

    public string[] Roles => new[]
        { Domain.Constants.OperationClaims.Admin, Admin, Write, CorporateCustomersOperationClaims.Update };

    public class
        UpdateCorporateCustomerCommandHandler : IRequestHandler<UpdateCorporateCustomerCommand,
            UpdatedCorporateCustomerResponse>
    {
        private readonly CorporateCustomerBusinessRules _corporateCustomerBusinessRules;
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly IMapper _mapper;

        public UpdateCorporateCustomerCommandHandler(
            ICorporateCustomerRepository corporateCustomerRepository,
            IMapper mapper,
            CorporateCustomerBusinessRules corporateCustomerBusinessRules
        )
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _mapper = mapper;
            _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
        }

        public async Task<UpdatedCorporateCustomerResponse> Handle(
            UpdateCorporateCustomerCommand request,
            CancellationToken cancellationToken
        )
        {
            await _corporateCustomerBusinessRules.CorporateCustomerTaxNoCanNotBeDuplicatedWhenInserted(request.TaxNo);

            CorporateCustomer mappedCorporateCustomer = _mapper.Map<CorporateCustomer>(request);
            CorporateCustomer updatedCorporateCustomer =
                await _corporateCustomerRepository.UpdateAsync(mappedCorporateCustomer);
            UpdatedCorporateCustomerResponse updatedCorporateCustomerDto =
                _mapper.Map<UpdatedCorporateCustomerResponse>(
                    updatedCorporateCustomer
                );
            return updatedCorporateCustomerDto;
        }
    }
}