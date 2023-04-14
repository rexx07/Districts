using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.RentalBranches.Constants;
using Modules.BaseApplication.Features.RentalBranches.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.RentalBranches.Constants.RentalBranchesOperationClaims;

namespace Modules.BaseApplication.Features.RentalBranches.Commands.Delete;

public class DeleteRentalBranchCommand : IRequest<DeletedRentalBranchResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, RentalBranchesOperationClaims.Delete };

    public class
        DeleteRentalBranchCommandHandler : IRequestHandler<DeleteRentalBranchCommand, DeletedRentalBranchResponse>
    {
        private readonly IMapper _mapper;
        private readonly RentalBranchBusinessRules _rentalBranchBusinessRules;
        private readonly IRentalBranchRepository _rentalBranchRepository;

        public DeleteRentalBranchCommandHandler(
            IRentalBranchRepository rentalBranchRepository,
            IMapper mapper,
            RentalBranchBusinessRules rentalBranchBusinessRules
        )
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
            _rentalBranchBusinessRules = rentalBranchBusinessRules;
        }

        public async Task<DeletedRentalBranchResponse> Handle(DeleteRentalBranchCommand request,
                                                              CancellationToken cancellationToken)
        {
            await _rentalBranchBusinessRules.RentalBranchIdShouldExistWhenSelected(request.Id);

            RentalBranch mappedRentalBranch = _mapper.Map<RentalBranch>(request);
            RentalBranch deletedRentalBranch = await _rentalBranchRepository.DeleteAsync(mappedRentalBranch);
            DeletedRentalBranchResponse deletedRentalBranchDto =
                _mapper.Map<DeletedRentalBranchResponse>(deletedRentalBranch);
            return deletedRentalBranchDto;
        }
    }
}