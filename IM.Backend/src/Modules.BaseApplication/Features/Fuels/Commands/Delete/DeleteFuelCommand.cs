using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Fuels.Constants;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Fuels.Constants.FuelsOperationClaims;

namespace Modules.BaseApplication.Features.Fuels.Commands.Delete;

public class DeleteFuelCommand : IRequest<DeletedFuelResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, FuelsOperationClaims.Delete };

    public class DeleteFuelCommandHandler : IRequestHandler<DeleteFuelCommand, DeletedFuelResponse>
    {
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;

        public DeleteFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<DeletedFuelResponse> Handle(DeleteFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel mappedFuel = _mapper.Map<Fuel>(request);
            Fuel deletedFuel = await _fuelRepository.DeleteAsync(mappedFuel);
            DeletedFuelResponse deletedFuelDto = _mapper.Map<DeletedFuelResponse>(deletedFuel);
            return deletedFuelDto;
        }
    }
}