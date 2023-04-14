using AutoMapper;
using Core.Domain.Entities.Security;
using MediatR;
using Modules.BaseApplication.Features.Users.Rules;

namespace Modules.BaseApplication.Features.Users.Queries.GetById;

public class GetByIdUserQuery : IRequest<GetByIdUserResponse>
{
    public int Id { get; set; }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IUserRepository _userRepository;

        public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper,
                                       UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserIdShouldExistWhenSelected(request.Id);

            User? user = await _userRepository.GetAsync(b => b.Id == request.Id);
            GetByIdUserResponse userDto = _mapper.Map<GetByIdUserResponse>(user);
            return userDto;
        }
    }
}