using Application.Features.UserOperationClaims.Rules;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimQuery : IRequest<GetByIdUserOperationClaimResponse>
{
    public int Id { get; set; }

    public class
        GetByIdUserOperationClaimQueryHandler : IRequestHandler<GetByIdUserOperationClaimQuery,
            GetByIdUserOperationClaimResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public GetByIdUserOperationClaimQueryHandler(
            IUserOperationClaimRepository userOperationClaimRepository,
            IMapper mapper,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules
        )
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<GetByIdUserOperationClaimResponse> Handle(
            GetByIdUserOperationClaimQuery request,
            CancellationToken cancellationToken
        )
        {
            await _userOperationClaimBusinessRules.UserOperationClaimIdShouldExistWhenSelected(request.Id);

            UserOperationClaim? userOperationClaim =
                await _userOperationClaimRepository.GetAsync(b => b.Id == request.Id);
            GetByIdUserOperationClaimResponse userOperationClaimDto =
                _mapper.Map<GetByIdUserOperationClaimResponse>(userOperationClaim);
            return userOperationClaimDto;
        }
    }
}