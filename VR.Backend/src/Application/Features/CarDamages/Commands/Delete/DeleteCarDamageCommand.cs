using Application.Features.CarDamages.Constants;
using Application.Features.CarDamages.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.CarDamages.Constants.CarDamagesOperationClaims;

namespace Application.Features.CarDamages.Commands.Delete;

public class DeleteCarDamageCommand : IRequest<DeletedCarDamageResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Domain.Constants.OperationClaims.Admin, Admin, Write, CarDamagesOperationClaims.Delete };

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

            CarDamage mappedCarDamage = _mapper.Map<CarDamage>(request);
            CarDamage deletedCarDamage = await _carDamageRepository.DeleteAsync(mappedCarDamage);
            DeletedCarDamageResponse deletedCarDamageDto = _mapper.Map<DeletedCarDamageResponse>(deletedCarDamage);
            return deletedCarDamageDto;
        }
    }
}