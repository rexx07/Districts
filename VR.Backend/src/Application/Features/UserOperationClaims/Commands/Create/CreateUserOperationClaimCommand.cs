using Application.Features.UserOperationClaims.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommand : IRequest<CreatedUserOperationClaimResponse>, ISecuredRequest
{
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class CreateUserOperationClaimCommandHandler
        : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public CreateUserOperationClaimCommandHandler(
            IUserOperationClaimRepository userOperationClaimRepository,
            IMapper mapper,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules
        )
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<CreatedUserOperationClaimResponse> Handle(
            CreateUserOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
            UserOperationClaim createdUserOperationClaim =
                await _userOperationClaimRepository.AddAsync(mappedUserOperationClaim);
            CreatedUserOperationClaimResponse createdUserOperationClaimDto =
                _mapper.Map<CreatedUserOperationClaimResponse>(
                    createdUserOperationClaim
                );
            return createdUserOperationClaimDto;
        }
    }
}