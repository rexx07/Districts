using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Cars.Constants;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Cars.Constants.CarsOperationClaims;

namespace Modules.BaseApplication.Features.Cars.Commands.Delete;

public class DeleteCarCommand : IRequest<DeletedCarResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, CarsOperationClaims.Delete };

    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, DeletedCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public DeleteCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<DeletedCarResponse> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            Vehicle mappedVehicle = _mapper.Map<Vehicle>(request);
            Vehicle deletedVehicle = await _carRepository.DeleteAsync(mappedVehicle);
            DeletedCarResponse deletedCarDto = _mapper.Map<DeletedCarResponse>(deletedVehicle);
            return deletedCarDto;
        }
    }
}