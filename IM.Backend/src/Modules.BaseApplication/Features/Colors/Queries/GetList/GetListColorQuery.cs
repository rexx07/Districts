﻿using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using MediatR;

namespace Modules.BaseApplication.Features.Colors.Queries.GetList;

public class GetListColorQuery : IRequest<GetListResponse<GetListColorListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListColorQueryHandler : IRequestHandler<GetListColorQuery, GetListResponse<GetListColorListItemDto>>
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public GetListColorQueryHandler(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListColorListItemDto>> Handle(
            GetListColorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Color> colors = await _colorRepository.GetListAsync(
                                          index: request.PageRequest.Page,
                                          size: request.PageRequest.PageSize
                                      );
            var mappedColorListModel = _mapper.Map<GetListResponse<GetListColorListItemDto>>(colors);
            return mappedColorListModel;
        }
    }
}