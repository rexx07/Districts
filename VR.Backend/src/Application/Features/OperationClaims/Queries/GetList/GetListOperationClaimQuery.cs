using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimQuery : IRequest<GetListResponse<GetListOperationClaimListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListOperationClaimQueryHandler
        : IRequestHandler<GetListOperationClaimQuery, GetListResponse<GetListOperationClaimListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOperationClaimRepository _operationClaimRepository;

        public GetListOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListOperationClaimListItemDto>> Handle(
            GetListOperationClaimQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync(
                                                            index: request.PageRequest.Page,
                                                            size: request.PageRequest.PageSize
                                                        );
            var mappedOperationClaimListModel =
                _mapper.Map<GetListResponse<GetListOperationClaimListItemDto>>(operationClaims);
            return mappedOperationClaimListModel;
        }
    }
}