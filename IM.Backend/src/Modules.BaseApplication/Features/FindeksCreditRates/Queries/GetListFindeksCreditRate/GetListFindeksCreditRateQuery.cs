using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using MediatR;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Queries.GetListFindeksCreditRate;

public class GetListFindeksCreditRateQuery : IRequest<GetListResponse<GetListFindeksCreditRateListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFindeksCreditRateQueryHandler
        : IRequestHandler<GetListFindeksCreditRateQuery, GetListResponse<GetListFindeksCreditRateListItemDto>>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;

        public GetListFindeksCreditRateQueryHandler(IFindeksCreditRateRepository findeksCreditRateRepository,
                                                    IMapper mapper)
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFindeksCreditRateListItemDto>> Handle(
            GetListFindeksCreditRateQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<FindeksCreditRate> findeksCreditRates = await _findeksCreditRateRepository.GetListAsync(
                                                                  index: request.PageRequest.Page,
                                                                  size: request.PageRequest.PageSize
                                                              );
            var mappedFindeksCreditRateListModel =
                _mapper.Map<GetListResponse<GetListFindeksCreditRateListItemDto>>(findeksCreditRates);
            return mappedFindeksCreditRateListModel;
        }
    }
}