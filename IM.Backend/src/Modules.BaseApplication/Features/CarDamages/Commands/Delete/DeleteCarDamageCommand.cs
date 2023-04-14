using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.CarDamages.Constants;
using Modules.BaseApplication.Features.CarDamages.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.CarDamages.Constants.CarDamagesOperationClaims;

namespace Modules.BaseApplication.Features.CarDamages.Commands.Delete;

public class DeleteCarDamageCommand : IRequest<DeletedCarDamageResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, CarDamagesOperationClaims.Delete };

    public class DeleteCarDamageCommandHandler : IRequestHandler<DeleteCarDamageCommand, DeletedCarDamageResponse>
    {
        private readonly CarDamageBusinessRules _carDamageBusinessRules;
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;

        public DeleteCarDamageCommandHandler(
            ICarDamageRepository carDamageRepository,
            IMapper mapper,
            CarDamageBusinessRules carDamageBusinessRules
        )
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
            _carDamageBusinessRules = carDamageBusinessRules;
        }

        public async Task<DeletedCarDamageResponse> Handle(DeleteCarDamageCommand request,
                                                           CancellationToken cancellationToken)
        {
            await _carDamageBusinessRules.CarDamageIdShouldExistWhenSelected(request.Id);

            VehicleDamage mappedVehicleDamage = _mapper.Map<VehicleDamage>(request);
            VehicleDamage deletedVehicleDamage = await _carDamageRepository.DeleteAsync(mappedVehicleDamage);
            DeletedCarDamageResponse deletedCarDamageDto = _mapper.Map<DeletedCarDamageResponse>(deletedVehicleDamage);
            return deletedCarDamageDto;
        }
    }
}