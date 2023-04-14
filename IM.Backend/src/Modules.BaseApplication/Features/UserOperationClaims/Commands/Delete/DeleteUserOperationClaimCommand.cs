using AutoMapper;
using Core.Domain.Entities.Security;
using MediatR;
using Modules.BaseApplication.Features.UserOperationClaims.Constants;
using Modules.BaseApplication.Features.UserOperationClaims.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Modules.BaseApplication.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommand : IRequest<DeletedUserOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, UserOperationClaimsOperationClaims.Delete };

    public class DeleteUserOperationClaimCommandHandler
        : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public DeleteUserOperationClaimCommandHandler(
            IUserOperationClaimRepository userOperationClaimRepository,
            IMapper mapper,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules
        )
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<DeletedUserOperationClaimResponse> Handle(
            DeleteUserOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            await _userOperationClaimBusinessRules.UserOperationClaimIdShouldExistWhenSelected(request.Id);

            UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
            UserOperationClaim deletedUserOperationClaim =
                await _userOperationClaimRepository.DeleteAsync(mappedUserOperationClaim);
            DeletedUserOperationClaimResponse deletedUserOperationClaimDto =
                _mapper.Map<DeletedUserOperationClaimResponse>(
                    deletedUserOperationClaim
                );
            return deletedUserOperationClaimDto;
        }
    }
}