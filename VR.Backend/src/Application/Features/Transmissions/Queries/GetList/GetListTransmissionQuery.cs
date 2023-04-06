﻿using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;

namespace Application.Features.Transmissions.Queries.GetList;

public class GetListTransmissionQuery : IRequest<GetListResponse<GetListTransmissionListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTransmissionQueryHandler
        : IRequestHandler<GetListTransmissionQuery, GetListResponse<GetListTransmissionListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITransmissionRepository _transmissionRepository;

        public GetListTransmissionQueryHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTransmissionListItemDto>> Handle(
            GetListTransmissionQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Transmission> transmissions = await _transmissionRepository.GetListAsync(
                                                        index: request.PageRequest.Page,
                                                        size: request.PageRequest.PageSize
                                                    );
            var mappedTransmissionListModel =
                _mapper.Map<GetListResponse<GetListTransmissionListItemDto>>(transmissions);
            return mappedTransmissionListModel;
        }
    }
}