﻿using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Dynamic;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListByDynamic;

public class GetListByDynamicModelQuery : IRequest<GetListResponse<GetListByDynamicModelListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class GetListModelByDynamicQueryHandler
        : IRequestHandler<GetListByDynamicModelQuery, GetListResponse<GetListByDynamicModelListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository;

        public GetListModelByDynamicQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicModelListItemDto>> Handle(
            GetListByDynamicModelQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(
                                          request.DynamicQuery,
                                          include: c =>
                                              c.Include(c => c.Brand).Include(c => c.Fuel).Include(c => c.Transmission),
                                          index: request.PageRequest.Page,
                                          size: request.PageRequest.PageSize
                                      );
            var mappedModelListModel = _mapper.Map<GetListResponse<GetListByDynamicModelListItemDto>>(models);
            return mappedModelListModel;
        }
    }
}