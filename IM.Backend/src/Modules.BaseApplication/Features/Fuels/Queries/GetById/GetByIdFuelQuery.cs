using Application.Features.Fuels.Rules;
using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Entities.Land;
using MediatR;

namespace Application.Features.Fuels.Queries.GetById;

public class GetByIdFuelQuery : IRequest<GetByIdFuelResponse>
{
    public int Id { get; set; }

    public class GetByIdFuelQueryHandler : IRequestHandler<GetByIdFuelQuery, GetByIdFuelResponse>
    {
        private readonly FuelBusinessRules _fuelBusinessRules;
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;

        public GetByIdFuelQueryHandler(IFuelRepository fuelRepository, FuelBusinessRules fuelBusinessRules,
                                       IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _fuelBusinessRules = fuelBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByIdFuelResponse> Handle(GetByIdFuelQuery request, CancellationToken cancellationToken)
        {
            await _fuelBusinessRules.FuelIdShouldExistWhenSelected(request.Id);

            Fuel? fuel = await _fuelRepository.GetAsync(f => f.Id == request.Id);
            GetByIdFuelResponse fuelDto = _mapper.Map<GetByIdFuelResponse>(fuel);
            return fuelDto;
        }
    }
}