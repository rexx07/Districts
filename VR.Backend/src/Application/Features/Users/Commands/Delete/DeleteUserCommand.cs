using Application.Features.Users.Constants;
using Application.Features.Users.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.Users.Constants.UsersOperationClaims;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<DeletedUserResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles =>
        new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, UsersOperationClaims.Delete };

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper,
                                        UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<DeletedUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserIdShouldExistWhenSelected(request.Id);

            User mappedUser = _mapper.Map<User>(request);
            User deletedUser = await _userRepository.DeleteAsync(mappedUser);
            DeletedUserResponse deletedUserDto = _mapper.Map<DeletedUserResponse>(deletedUser);
            return deletedUserDto;
        }
    }
}