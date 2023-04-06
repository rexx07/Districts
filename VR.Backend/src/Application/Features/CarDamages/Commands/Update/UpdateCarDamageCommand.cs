using Application.Features.CarDamages.Constants;
using Application.Features.CarDamages.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.CarDamages.Constants.CarDamagesOperationClaims;

namespace Application.Features.CarDamages.Commands.Update;

public class UpdateCarDamageCommand : IRequest<UpdatedCarDamageResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }

    public string[] Roles => new[]
        { Domain.Constants.OperationClaims.Admin, Admin, Write, CarDamagesOperationClaims.Update };

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
            CarDamage mappedCarDamage = _mapper.Map<CarDamage>(request);
            CarDamage updatedCarDamage = await _carDamageRepository.UpdateAsync(mappedCarDamage);
            UpdatedCarDamageResponse updatedCarDamageDto = _mapper.Map<UpdatedCarDamageResponse>(updatedCarDamage);
            return updatedCarDamageDto;
        }
    }
}