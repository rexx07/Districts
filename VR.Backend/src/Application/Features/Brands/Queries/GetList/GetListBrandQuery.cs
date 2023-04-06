using Application.Pipelines.Caching;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;

namespace Application.Features.Brands.Queries.GetList;

public class GetListBrandQuery : IRequest<GetListResponse<GetListBrandListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }

    public string CacheKey => $"GetListBrands({PageRequest.Page},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetBrands";

    public TimeSpan? SlidingExpiration { get; }

    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, GetListResponse<GetListBrandListItemDto>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBrandListItemDto>> Handle(
            GetListBrandQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Brand> brands = await _brandRepository.GetListAsync(
                                          index: request.PageRequest.Page,
                                          size: request.PageRequest.PageSize
                                      );
            var mappedBrandListModel = _mapper.Map<GetListResponse<GetListBrandListItemDto>>(brands);
            return mappedBrandListModel;
        }
    }
}