using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.CarDamages.Constants;
using Modules.BaseApplication.Features.CarDamages.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.CarDamages.Constants.CarDamagesOperationClaims;

namespace Modules.BaseApplication.Features.CarDamages.Commands.Update;

public class UpdateCarDamageCommand : IRequest<UpdatedCarDamageResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, CarDamagesOperationClaims.Update };

    public class UpdateCarDamageCommandHandler : IRequestHandler<UpdateCarDamageCommand, UpdatedCarDamageResponse>
    {
        private readonly CarDamageBusinessRules _carDamageBusinessRules;
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;

        public UpdateCarDamageCommandHandler(
            ICarDamageRepository carDamageRepository,
            IMapper mapper,
            CarDamageBusinessRules carDamageBusinessRules
        )
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
            _carDamageBusinessRules = carDamageBusinessRules;
        }

        public async Task<UpdatedCarDamageResponse> Handle(UpdateCarDamageCommand request,
                                                           CancellationToken cancellationToken)
        {
            VehicleDamage mappedVehicleDamage = _mapper.Map<VehicleDamage>(request);
            VehicleDamage updatedVehicleDamage = await _carDamageRepository.UpdateAsync(mappedVehicleDamage);
            UpdatedCarDamageResponse updatedCarDamageDto = _mapper.Map<UpdatedCarDamageResponse>(updatedVehicleDamage);
            return updatedCarDamageDto;
        }
    }
}