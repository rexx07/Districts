using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQuery : IRequest<GetListResponse<GetListUserListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListResponse<GetListUserListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListUserListItemDto>> Handle(
            GetListUserQuery request, CancellationToken cancellationToken)
        {
            IPaginate<User> users =
                await _userRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            var mappedUserListModel = _mapper.Map<GetListResponse<GetListUserListItemDto>>(users);
            return mappedUserListModel;
        }
    }
}