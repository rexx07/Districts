using Application.Features.IndividualCustomers.Rules;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.IndividualCustomers.Queries.GetById;

public class GetByIdIndividualCustomerQuery : IRequest<GetByIdIndividualCustomerResponse>
{
    public int Id { get; set; }

    public class
        GetByIdIndividualCustomerQueryHandler : IRequestHandler<GetByIdIndividualCustomerQuery,
            GetByIdIndividualCustomerResponse>
    {
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;

        public GetByIdIndividualCustomerQueryHandler(
            IIndividualCustomerRepository individualCustomerRepository,
            IndividualCustomerBusinessRules individualCustomerBusinessRules,
            IMapper mapper
        )
        {
            _individualCustomerRepository = individualCustomerRepository;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByIdIndividualCustomerResponse> Handle(
            GetByIdIndividualCustomerQuery request,
            CancellationToken cancellationToken
        )
        {
            IndividualCustomer? individualCustomer =
                await _individualCustomerRepository.GetAsync(b => b.Id == request.Id);
            await _individualCustomerBusinessRules.IndividualCustomerShouldBeExist(individualCustomer);
            GetByIdIndividualCustomerResponse individualCustomerDto =
                _mapper.Map<GetByIdIndividualCustomerResponse>(individualCustomer);
            return individualCustomerDto;
        }
    }
}