using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.Rentals.Constants;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Rentals.Constants.RentalsOperationClaims;

namespace Modules.BaseApplication.Features.Rentals.Commands.Delete;

public class DeleteRentalCommand : IRequest<DeletedRentalResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, RentalsOperationClaims.Delete };

    public class DeleteRentalCommandHandler : IRequestHandler<DeleteRentalCommand, DeletedRentalResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRentalRepository _rentalRepository;

        public DeleteRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }

        public async Task<DeletedRentalResponse> Handle(DeleteRentalCommand request,
                                                        CancellationToken cancellationToken)
        {
            Rental mappedRental = _mapper.Map<Rental>(request);
            Rental deletedRental = await _rentalRepository.DeleteAsync(mappedRental);
            DeletedRentalResponse deletedRentalDto = _mapper.Map<DeletedRentalResponse>(deletedRental);
            return deletedRentalDto;
        }
    }
}