using Application.Features.CarDamages.Rules;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;

namespace Application.Features.CarDamages.Queries.GetById;

public class GetByIdCarDamageQuery : IRequest<GetByIdCarDamageResponse>
{
    public int Id { get; set; }

    public class GetByIdCarDamageQueryHandler : IRequestHandler<GetByIdCarDamageQuery, GetByIdCarDamageResponse>
    {
        private readonly CarDamageBusinessRules _carDamageBusinessRules;
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;

        public GetByIdCarDamageQueryHandler(
            ICarDamageRepository carDamageRepository,
            CarDamageBusinessRules carDamageBusinessRules,
            IMapper mapper
        )
        {
            _carDamageRepository = carDamageRepository;
            _carDamageBusinessRules = carDamageBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByIdCarDamageResponse> Handle(GetByIdCarDamageQuery request,
                                                           CancellationToken cancellationToken)
        {
            await _carDamageBusinessRules.CarDamageIdShouldExistWhenSelected(request.Id);

            CarDamage? carDamage = await _carDamageRepository.GetAsync(b => b.Id == request.Id);
            GetByIdCarDamageResponse carDamageDto = _mapper.Map<GetByIdCarDamageResponse>(carDamage);
            return carDamageDto;
        }
    }
}