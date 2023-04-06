using Application.Features.Rentals.Constants;
using Application.Pipelines.Authorization;
using Application.Services.CarService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.Rentals.Constants.RentalsOperationClaims;

namespace Application.Features.Rentals.Commands.PickUp;

public class PickUpRentalCommand : IRequest<PickUpRentalResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int RentEndRentalBranchId { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int RentEndKilometer { get; set; }

    public string[] Roles => new[]
        { Domain.Constants.OperationClaims.Admin, Admin, Write, RentalsOperationClaims.Update };

    public class PickUpRentalCommandHandler : IRequestHandler<PickUpRentalCommand, PickUpRentalResponse>
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        private readonly IRentalRepository _rentalRepository;

        public PickUpRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper, ICarService carService)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _carService = carService;
        }

        public async Task<PickUpRentalResponse> Handle(PickUpRentalCommand request, CancellationToken cancellationToken)
        {
            Rental rental = await _rentalRepository.GetAsync(r => r.Id == request.Id);
            //rental.RentEndRentalBranchId = request.RentEndRentalBranchId;
            rental.RentEndKilometer = request.RentEndKilometer;
            rental.ReturnDate = request.ReturnDate;

            await _carService.PickUpCar(rental);

            Rental updatedRental = await _rentalRepository.UpdateAsync(rental);
            PickUpRentalResponse updatedRentalDto = _mapper.Map<PickUpRentalResponse>(updatedRental);
            return updatedRentalDto;
        }
    }
}