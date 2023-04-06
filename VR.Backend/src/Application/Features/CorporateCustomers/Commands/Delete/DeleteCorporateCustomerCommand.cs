using Application.Features.CorporateCustomers.Constants;
using Application.Features.CorporateCustomers.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.CorporateCustomers.Constants.CorporateCustomersOperationClaims;

namespace Application.Features.CorporateCustomers.Commands.Delete;

public class DeleteCorporateCustomerCommand : IRequest<DeletedCorporateCustomerResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Domain.Constants.OperationClaims.Admin, Admin, Write, CorporateCustomersOperationClaims.Delete };

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