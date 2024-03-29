﻿using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Fuels.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Fuels.Constants.FuelsOperationClaims;

namespace Modules.BaseApplication.Features.Fuels.Commands.Create;

public class CreateFuelCommand : IRequest<CreatedFuelResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class CreateFuelCommandHandler : IRequestHandler<CreateFuelCommand, CreatedFuelResponse>
    {
        private readonly FuelBusinessRules _fuelBusinessRules;
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;

        public CreateFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper,
                                        FuelBusinessRules fuelBusinessRules)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
            _fuelBusinessRules = fuelBusinessRules;
        }

        public async Task<CreatedFuelResponse> Handle(CreateFuelCommand request, CancellationToken cancellationToken)
        {
            await _fuelBusinessRules.FuelNameCanNotBeDuplicatedWhenInserted(request.Name);

            Fuel mappedFuel = _mapper.Map<Fuel>(request);
            Fuel createdFuel = await _fuelRepository.AddAsync(mappedFuel);
            CreatedFuelResponse createdFuelDto = _mapper.Map<CreatedFuelResponse>(createdFuel);
            return createdFuelDto;
        }
    }
}