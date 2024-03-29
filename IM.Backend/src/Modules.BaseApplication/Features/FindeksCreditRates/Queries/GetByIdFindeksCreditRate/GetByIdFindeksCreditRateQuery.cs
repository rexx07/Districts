using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.FindeksCreditRates.Rules;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;

public class GetByIdFindeksCreditRateQuery : IRequest<GetByIdFindeksCreditRateResponse>
{
    public int Id { get; set; }

    public class
        GetByIdFindeksCreditRateQueryHandler : IRequestHandler<GetByIdFindeksCreditRateQuery,
            GetByIdFindeksCreditRateResponse>
    {
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;

        public GetByIdFindeksCreditRateQueryHandler(
            IFindeksCreditRateRepository findeksCreditRateRepository,
            FindeksCreditRateBusinessRules findeksCreditRateBusinessRules,
            IMapper mapper
        )
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByIdFindeksCreditRateResponse> Handle(
            GetByIdFindeksCreditRateQuery request,
            CancellationToken cancellationToken
        )
        {
            await _findeksCreditRateBusinessRules.FindeksCreditRateIdShouldExistWhenSelected(request.Id);

            FindeksCreditRate? findeksCreditRate = await _findeksCreditRateRepository.GetAsync(b => b.Id == request.Id);
            GetByIdFindeksCreditRateResponse findeksCreditRateDto =
                _mapper.Map<GetByIdFindeksCreditRateResponse>(findeksCreditRate);
            return findeksCreditRateDto;
        }
    }
}