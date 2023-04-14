using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.CorporateCustomers.Constants;
using Modules.BaseApplication.Features.CorporateCustomers.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.CorporateCustomers.Constants.CorporateCustomersOperationClaims;

namespace Modules.BaseApplication.Features.CorporateCustomers.Commands.Delete;

public class DeleteCorporateCustomerCommand : IRequest<DeletedCorporateCustomerResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, CorporateCustomersOperationClaims.Delete };

    public class
        DeleteCorporateCustomerCommandHandler : IRequestHandler<DeleteCorporateCustomerCommand,
            DeletedCorporateCustomerResponse>
    {
        private readonly CorporateCustomerBusinessRules _corporateCustomerBusinessRules;
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly IMapper _mapper;

        public DeleteCorporateCustomerCommandHandler(
            ICorporateCustomerRepository corporateCustomerRepository,
            IMapper mapper,
            CorporateCustomerBusinessRules corporateCustomerBusinessRules
        )
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _mapper = mapper;
            _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
        }

        public async Task<DeletedCorporateCustomerResponse> Handle(
            DeleteCorporateCustomerCommand request,
            CancellationToken cancellationToken
        )
        {
            await _corporateCustomerBusinessRules.CorporateCustomerIdShouldExistWhenSelected(request.Id);

            CorporateCustomer mappedCorporateCustomer = _mapper.Map<CorporateCustomer>(request);
            CorporateCustomer deletedCorporateCustomer =
                await _corporateCustomerRepository.DeleteAsync(mappedCorporateCustomer);
            DeletedCorporateCustomerResponse deletedCorporateCustomerDto =
                _mapper.Map<DeletedCorporateCustomerResponse>(
                    deletedCorporateCustomer
                );
            return deletedCorporateCustomerDto;
        }
    }
}