using AutoMapper;
using Core.Domain.Entities.Security;
using MediatR;
using Modules.BaseApplication.Features.OperationClaims.Constants;
using Modules.BaseApplication.Features.OperationClaims.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Modules.BaseApplication.Features.OperationClaims.Commands.Delete;

public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, OperationClaimsOperationClaims.Delete };

    public class
        DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimResponse>
    {
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
        private readonly IOperationClaimRepository _operationClaimRepository;

        public DeleteOperationClaimCommandHandler(
            IOperationClaimRepository operationClaimRepository,
            IMapper mapper,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<DeletedOperationClaimResponse> Handle(DeleteOperationClaimCommand request,
                                                                CancellationToken cancellationToken)
        {
            await _operationClaimBusinessRules.OperationClaimIdShouldExistWhenSelected(request.Id);

            OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
            OperationClaim deletedOperationClaim = await _operationClaimRepository.DeleteAsync(mappedOperationClaim);
            DeletedOperationClaimResponse deletedOperationClaimDto =
                _mapper.Map<DeletedOperationClaimResponse>(deletedOperationClaim);
            return deletedOperationClaimDto;
        }
    }
}