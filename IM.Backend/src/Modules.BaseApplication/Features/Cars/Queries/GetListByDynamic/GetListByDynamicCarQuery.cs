﻿using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Dynamic;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using MediatR;

namespace Modules.BaseApplication.Features.Cars.Queries.GetListByDynamic;

public class GetListByDynamicCarQuery : IRequest<GetListResponse<GetListByDynamicCarListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class GetListCarByDynamicQueryHandler
        : IRequestHandler<GetListByDynamicCarQuery, GetListResponse<GetListByDynamicCarListItemDto>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetListCarByDynamicQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicCarListItemDto>> Handle(
            GetListByDynamicCarQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Vehicle> cars = await _carRepository.GetListByDynamicAsync(
                                      request.DynamicQuery,
                                      include: c =>
                                          c.Include(c => c.Model).Include(c => c.Model.Brand).Include(c => c.Color),
                                      index: request.PageRequest.Page,
                                      size: request.PageRequest.PageSize
                                  );
            var mappedCarListModel = _mapper.Map<GetListResponse<GetListByDynamicCarListItemDto>>(cars);
            return mappedCarListModel;
        }
    }
}