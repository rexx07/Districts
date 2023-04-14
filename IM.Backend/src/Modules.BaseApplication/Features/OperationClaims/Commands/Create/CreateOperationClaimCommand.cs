using AutoMapper;
using Core.Domain.Entities.Security;
using MediatR;
using Modules.BaseApplication.Features.OperationClaims.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Modules.BaseApplication.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class
        CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimResponse>
    {
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
        private readonly IOperationClaimRepository _operationClaimRepository;

        public CreateOperationClaimCommandHandler(
            IOperationClaimRepository operationClaimRepository,
            IMapper mapper,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<CreatedOperationClaimResponse> Handle(CreateOperationClaimCommand request,
                                                                CancellationToken cancellationToken)
        {
            OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
            OperationClaim createdOperationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);
            CreatedOperationClaimResponse createdOperationClaimDto =
                _mapper.Map<CreatedOperationClaimResponse>(createdOperationClaim);
            return createdOperationClaimDto;
        }
    }
}