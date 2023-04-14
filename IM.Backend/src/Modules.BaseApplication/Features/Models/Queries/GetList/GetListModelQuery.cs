﻿using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using MediatR;
using Modules.BaseApplication.Pipelines.Caching;

namespace Modules.BaseApplication.Features.Models.Queries.GetList;

public class GetListModelQuery : IRequest<GetListResponse<GetListModelListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetListModels({PageRequest.Page},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetModels";
    public TimeSpan? SlidingExpiration { get; set; }

    public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, GetListResponse<GetListModelListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository;

        public GetListModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListModelListItemDto>> Handle(
            GetListModelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Model> models = await _modelRepository.GetListAsync(
                                          include: c =>
                                              c.Include(c => c.Brand).Include(c => c.Fuel).Include(c => c.Transmission),
                                          index: request.PageRequest.Page,
                                          size: request.PageRequest.PageSize
                                      );
            var mappedModelListModel = _mapper.Map<GetListResponse<GetListModelListItemDto>>(models);
            return mappedModelListModel;
        }
    }
}