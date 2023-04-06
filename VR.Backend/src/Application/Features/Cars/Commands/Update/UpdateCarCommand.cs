using Application.Features.Cars.Constants;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.Cars.Constants.CarsOperationClaims;

namespace Application.Features.Cars.Commands.Update;

public class UpdateCarCommand : IRequest<UpdatedCarResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public int RentalBranchId { get; set; }
    public CarState CarState { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindeksCreditRate { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, CarsOperationClaims.Update };

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
            Car mappedCar = _mapper.Map<Car>(request);
            Car updatedCar = await _carRepository.UpdateAsync(mappedCar);
            UpdatedCarResponse updatedCarDto = _mapper.Map<UpdatedCarResponse>(updatedCar);
            return updatedCarDto;
        }
    }
}