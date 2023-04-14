using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Domain.Enums;
using MediatR;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Cars.Constants.CarsOperationClaims;

namespace Modules.BaseApplication.Features.Cars.Commands.Create;

public class CreateCarCommand : IRequest<CreatedCarResponse>, ISecuredRequest
{
    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public int RentalBranchId { get; set; }
    public VehicleState CarState { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindeksCreditRate { get; set; }

    public string[] Roles => new[] { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CreatedCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CreateCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CreatedCarResponse> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Vehicle mappedVehicle = _mapper.Map<Vehicle>(request);
            Vehicle createdVehicle = await _carRepository.AddAsync(mappedVehicle);
            CreatedCarResponse createdCarDto = _mapper.Map<CreatedCarResponse>(createdVehicle);
            return createdCarDto;
        }
    }
}