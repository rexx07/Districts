using Application.Features.RentalBranches.Rules;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.RentalBranches.Queries.GetById;

public class GetByIdRentalBranchQuery : IRequest<GetByIdRentalBranchResponse>
{
    public int Id { get; set; }

    public class
        GetByIdRentalBranchQueryHandler : IRequestHandler<GetByIdRentalBranchQuery, GetByIdRentalBranchResponse>
    {
        private readonly IMapper _mapper;
        private readonly RentalBranchBusinessRules _rentalBranchBusinessRules;
        private readonly IRentalBranchRepository _rentalBranchRepository;

        public GetByIdRentalBranchQueryHandler(
            IRentalBranchRepository rentalBranchRepository,
            IMapper mapper,
            RentalBranchBusinessRules rentalBranchBusinessRules
        )
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
            _rentalBranchBusinessRules = rentalBranchBusinessRules;
        }

        public async Task<GetByIdRentalBranchResponse> Handle(GetByIdRentalBranchQuery request,
                                                              CancellationToken cancellationToken)
        {
            await _rentalBranchBusinessRules.RentalBranchIdShouldExistWhenSelected(request.Id);

            RentalBranch? rentalBranch = await _rentalBranchRepository.GetAsync(b => b.Id == request.Id);
            GetByIdRentalBranchResponse rentalBranchDto = _mapper.Map<GetByIdRentalBranchResponse>(rentalBranch);
            return rentalBranchDto;
        }
    }
}