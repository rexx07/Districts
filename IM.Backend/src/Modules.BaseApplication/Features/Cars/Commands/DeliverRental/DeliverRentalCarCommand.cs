using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Domain.Enums;
using MediatR;
using Modules.BaseApplication.Features.Cars.Constants;
using Modules.BaseApplication.Features.Cars.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Cars.Constants.CarsOperationClaims;

namespace Modules.BaseApplication.Features.Cars.Commands.DeliverRental;

public class DeliverRentalCarCommand : IRequest<DeliveredCarResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, CarsOperationClaims.Update };

    public class DeliverRentalCarCommandHandler : IRequestHandler<DeliverRentalCarCommand, DeliveredCarResponse>
    {
        private readonly CarBusinessRules _carBusinessRules;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public DeliverRentalCarCommandHandler(ICarRepository carRepository, CarBusinessRules carBusinessRules,
                                              IMapper mapper)
        {
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
            _mapper = mapper;
        }

        public async Task<DeliveredCarResponse> Handle(DeliverRentalCarCommand request,
                                                       CancellationToken cancellationToken)
        {
            await _carBusinessRules.CarCanNotBeRentWhenIsInMaintenance(request.Id);
            await _carBusinessRules.CarCanNotBeMaintainWhenIsRented(request.Id);

            Vehicle? updatedCar = await _carRepository.GetAsync(c => c.Id == request.Id);
            updatedCar.CarState = VehicleState.Rented;
            await _carRepository.UpdateAsync(updatedCar);
            DeliveredCarResponse? updatedCarDto = _mapper.Map<DeliveredCarResponse>(updatedCar);
            return updatedCarDto;
        }
    }
}