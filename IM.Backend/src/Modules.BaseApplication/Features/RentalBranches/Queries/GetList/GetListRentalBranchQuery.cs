using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using MediatR;

namespace Modules.BaseApplication.Features.RentalBranches.Queries.GetList;

public class GetListRentalBranchQuery : IRequest<GetListResponse<GetListRentalBranchListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListRentalBranchQueryHandler
        : IRequestHandler<GetListRentalBranchQuery, GetListResponse<GetListRentalBranchListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRentalBranchRepository _rentalBranchRepository;

        public GetListRentalBranchQueryHandler(IRentalBranchRepository rentalBranchRepository, IMapper mapper)
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListRentalBranchListItemDto>> Handle(
            GetListRentalBranchQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<RentalBranch> rentalBranchs = await _rentalBranchRepository.GetListAsync(
                                                        index: request.PageRequest.Page,
                                                        size: request.PageRequest.PageSize
                                                    );
            var mappedRentalBranchListModel =
                _mapper.Map<GetListResponse<GetListRentalBranchListItemDto>>(rentalBranchs);
            return mappedRentalBranchListModel;
        }
    }
}