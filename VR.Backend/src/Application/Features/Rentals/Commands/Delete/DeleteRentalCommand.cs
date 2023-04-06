using Application.Features.Rentals.Constants;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.Rentals.Constants.RentalsOperationClaims;

namespace Application.Features.Rentals.Commands.Delete;

public class DeleteRentalCommand : IRequest<DeletedRentalResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Domain.Constants.OperationClaims.Admin, Admin, Write, RentalsOperationClaims.Delete };

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