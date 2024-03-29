﻿using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Domain.Enums;
using MediatR;
using Modules.BaseApplication.Features.Cars.Constants;
using Modules.BaseApplication.Features.Cars.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Cars.Constants.CarsOperationClaims;

namespace Modules.BaseApplication.Features.Cars.Commands.Maintain;

public class MaintainCarCommand : IRequest<MaintainedCarResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, CarsOperationClaims.Update };

    public class MaintainCarCommandHandler : IRequestHandler<MaintainCarCommand, MaintainedCarResponse>
    {
        private readonly CarBusinessRules _carBusinessRules;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public MaintainCarCommandHandler(ICarRepository carRepository, CarBusinessRules carBusinessRules,
                                         IMapper mapper)
        {
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
            _mapper = mapper;
        }

        public async Task<MaintainedCarResponse> Handle(MaintainCarCommand request, CancellationToken cancellationToken)
        {
            await _carBusinessRules.CarIdShouldExistWhenSelected(request.Id);
            await _carBusinessRules.CarCanNotBeMaintainWhenIsRented(request.Id);

            Vehicle? updatedCar = await _carRepository.GetAsync(c => c.Id == request.Id);
            updatedCar.CarState = VehicleState.Maintenance;
            await _carRepository.UpdateAsync(updatedCar);
            MaintainedCarResponse? updatedCarDto = _mapper.Map<MaintainedCarResponse>(updatedCar);
            return updatedCarDto;
        }
    }
}