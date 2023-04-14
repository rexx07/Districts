using AutoMapper;
using Core.Domain.Entities.Security;
using MediatR;
using Modules.BaseApplication.Features.OperationClaims.Rules;

namespace Modules.BaseApplication.Features.OperationClaims.Queries.GetById;

public class GetByIdOperationClaimQuery : IRequest<GetByIdOperationClaimResponse>
{
    public int Id { get; set; }

    public class
        GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, GetByIdOperationClaimResponse>
    {
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
        private readonly IOperationClaimRepository _operationClaimRepository;

        public GetByIdOperationClaimQueryHandler(
            IOperationClaimRepository operationClaimRepository,
            IMapper mapper,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<GetByIdOperationClaimResponse> Handle(GetByIdOperationClaimQuery request,
                                                                CancellationToken cancellationToken)
        {
            await _operationClaimBusinessRules.OperationClaimIdShouldExistWhenSelected(request.Id);

            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(b => b.Id == request.Id);
            GetByIdOperationClaimResponse
                operationClaimDto = _mapper.Map<GetByIdOperationClaimResponse>(operationClaim);
            return operationClaimDto;
        }
    }
}