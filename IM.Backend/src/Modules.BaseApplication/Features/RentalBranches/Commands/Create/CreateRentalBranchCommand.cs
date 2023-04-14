using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Modules.BaseApplication.Features.RentalBranches.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.RentalBranches.Constants.RentalBranchesOperationClaims;

namespace Modules.BaseApplication.Features.RentalBranches.Commands.Create;

public class CreateRentalBranchCommand : IRequest<CreatedRentalBranchResponse>, ISecuredRequest
{
    public City City { get; set; }

    public string[] Roles => new[] { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class
        CreateRentalBranchCommandHandler : IRequestHandler<CreateRentalBranchCommand, CreatedRentalBranchResponse>
    {
        private readonly IMapper _mapper;
        private readonly RentalBranchBusinessRules _rentalBranchBusinessRules;
        private readonly IRentalBranchRepository _rentalBranchRepository;

        public CreateRentalBranchCommandHandler(
            IRentalBranchRepository rentalBranchRepository,
            IMapper mapper,
            RentalBranchBusinessRules rentalBranchBusinessRules
        )
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
            _rentalBranchBusinessRules = rentalBranchBusinessRules;
        }

        public async Task<CreatedRentalBranchResponse> Handle(CreateRentalBranchCommand request,
                                                              CancellationToken cancellationToken)
        {
            RentalBranch mappedRentalBranch = _mapper.Map<RentalBranch>(request);
            RentalBranch createdRentalBranch = await _rentalBranchRepository.AddAsync(mappedRentalBranch);
            CreatedRentalBranchResponse createdRentalBranchDto =
                _mapper.Map<CreatedRentalBranchResponse>(createdRentalBranch);
            return createdRentalBranchDto;
        }
    }
}