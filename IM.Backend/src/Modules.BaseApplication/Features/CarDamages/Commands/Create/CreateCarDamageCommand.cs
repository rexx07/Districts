using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.CarDamages.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.CarDamages.Constants.CarDamagesOperationClaims;

namespace Modules.BaseApplication.Features.CarDamages.Commands.Create;

public class CreateCarDamageCommand : IRequest<CreatedCarDamageResponse>, ISecuredRequest
{
    public int CarId { get; set; }
    public string DamageDescription { get; set; }

    public string[] Roles => new[] { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class CreateCarDamageCommandHandler : IRequestHandler<CreateCarDamageCommand, CreatedCarDamageResponse>
    {
        private readonly CarDamageBusinessRules _carDamageBusinessRules;
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;

        public CreateCarDamageCommandHandler(
            ICarDamageRepository carDamageRepository,
            IMapper mapper,
            CarDamageBusinessRules carDamageBusinessRules
        )
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
            _carDamageBusinessRules = carDamageBusinessRules;
        }

        public async Task<CreatedCarDamageResponse> Handle(CreateCarDamageCommand request,
                                                           CancellationToken cancellationToken)
        {
            VehicleDamage mappedVehicleDamage = _mapper.Map<VehicleDamage>(request);
            VehicleDamage createdVehicleDamage = await _carDamageRepository.AddAsync(mappedVehicleDamage);
            CreatedCarDamageResponse createdCarDamageResponse = _mapper.Map<CreatedCarDamageResponse>(createdVehicleDamage);
            return createdCarDamageResponse;
        }
    }
}