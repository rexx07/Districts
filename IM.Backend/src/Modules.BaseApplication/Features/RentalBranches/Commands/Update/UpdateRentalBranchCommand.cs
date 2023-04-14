using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Modules.BaseApplication.Features.RentalBranches.Constants;
using Modules.BaseApplication.Features.RentalBranches.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.RentalBranches.Constants.RentalBranchesOperationClaims;

namespace Modules.BaseApplication.Features.RentalBranches.Commands.Update;

public class UpdateRentalBranchCommand : IRequest<UpdatedRentalBranchResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public City City { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, RentalBranchesOperationClaims.Update };

    public class
        UpdateRentalBranchCommandHandler : IRequestHandler<UpdateRentalBranchCommand, UpdatedRentalBranchResponse>
    {
        private readonly IMapper _mapper;
        private readonly RentalBranchBusinessRules _rentalBranchBusinessRules;
        private readonly IRentalBranchRepository _rentalBranchRepository;

        public UpdateRentalBranchCommandHandler(
            IRentalBranchRepository rentalBranchRepository,
            IMapper mapper,
            RentalBranchBusinessRules rentalBranchBusinessRules
        )
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
            _rentalBranchBusinessRules = rentalBranchBusinessRules;
        }

        public async Task<UpdatedRentalBranchResponse> Handle(UpdateRentalBranchCommand request,
                                                              CancellationToken cancellationToken)
        {
            RentalBranch mappedRentalBranch = _mapper.Map<RentalBranch>(request);
            RentalBranch updatedRentalBranch = await _rentalBranchRepository.UpdateAsync(mappedRentalBranch);
            UpdatedRentalBranchResponse updatedRentalBranchDto =
                _mapper.Map<UpdatedRentalBranchResponse>(updatedRentalBranch);
            return updatedRentalBranchDto;
        }
    }
}