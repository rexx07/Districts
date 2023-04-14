using AutoMapper;
using Core.Domain.Entities.Security;
using MediatR;
using Modules.BaseApplication.Features.UserOperationClaims.Constants;
using Modules.BaseApplication.Features.UserOperationClaims.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Modules.BaseApplication.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommand : IRequest<UpdatedUserOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, UserOperationClaimsOperationClaims.Update };

    public class UpdateUserOperationClaimCommandHandler
        : IRequestHandler<UpdateUserOperationClaimCommand, UpdatedUserOperationClaimResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UpdateUserOperationClaimCommandHandler(
            IUserOperationClaimRepository userOperationClaimRepository,
            IMapper mapper,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules
        )
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<UpdatedUserOperationClaimResponse> Handle(
            UpdateUserOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
            UserOperationClaim updatedUserOperationClaim =
                await _userOperationClaimRepository.UpdateAsync(mappedUserOperationClaim);
            UpdatedUserOperationClaimResponse updatedUserOperationClaimDto =
                _mapper.Map<UpdatedUserOperationClaimResponse>(
                    updatedUserOperationClaim
                );
            return updatedUserOperationClaimDto;
        }
    }
}