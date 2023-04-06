using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetList;

public class GetListUserOperationClaimQuery : IRequest<GetListResponse<GetListUserOperationClaimListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListUserOperationClaimQueryHandler
        : IRequestHandler<GetListUserOperationClaimQuery, GetListResponse<GetListUserOperationClaimListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public GetListUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository,
                                                     IMapper mapper)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListUserOperationClaimListItemDto>> Handle(
            GetListUserOperationClaimQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(
                                                                    index: request.PageRequest.Page,
                                                                    size: request.PageRequest.PageSize
                                                                );
            var mappedUserOperationClaimListModel =
                _mapper.Map<GetListResponse<GetListUserOperationClaimListItemDto>>(userOperationClaims);
            return mappedUserOperationClaimListModel;
        }
    }
}