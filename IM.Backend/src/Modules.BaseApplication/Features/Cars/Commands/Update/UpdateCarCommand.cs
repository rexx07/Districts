using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Domain.Enums;
using MediatR;
using Modules.BaseApplication.Features.Cars.Constants;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Cars.Constants.CarsOperationClaims;

namespace Modules.BaseApplication.Features.Cars.Commands.Update;

public class UpdateCarCommand : IRequest<UpdatedCarResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public int RentalBranchId { get; set; }
    public VehicleState CarState { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindeksCreditRate { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, CarsOperationClaims.Update };

    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, UpdatedCarResponse>
    {
        public UpdateCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        private ICarRepository _carRepository { get; }
        private IMapper _mapper { get; }

        public async Task<UpdatedCarResponse> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            Vehicle mappedVehicle = _mapper.Map<Vehicle>(request);
            Vehicle updatedVehicle = await _carRepository.UpdateAsync(mappedVehicle);
            UpdatedCarResponse updatedCarDto = _mapper.Map<UpdatedCarResponse>(updatedVehicle);
            return updatedCarDto;
        }
    }
}