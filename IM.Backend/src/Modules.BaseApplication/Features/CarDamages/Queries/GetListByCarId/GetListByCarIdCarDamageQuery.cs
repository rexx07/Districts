﻿using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using MediatR;

namespace Modules.BaseApplication.Features.CarDamages.Queries.GetListByCarId;

public class GetListByCarIdCarDamageQuery : IRequest<GetListResponse<GetListByCarIdCarDamageListItemDto>>
{
    public int CarId { get; set; }
    public PageRequest PageRequest { get; set; }

    public class GetListByCarIdCarDamageQueryHandler
        : IRequestHandler<GetListByCarIdCarDamageQuery, GetListResponse<GetListByCarIdCarDamageListItemDto>>
    {
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;

        public GetListByCarIdCarDamageQueryHandler(ICarDamageRepository carDamageRepository, IMapper mapper)
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByCarIdCarDamageListItemDto>> Handle(
            GetListByCarIdCarDamageQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<VehicleDamage> carDamages = await _carDamageRepository.GetListAsync(
                                                  predicate: c => c.CarId == request.CarId,
                                                  include: c =>
                                                      c.Include(c => c.Car).Include(c => c.Car.Model)
                                                       .Include(c => c.Car.Model.Brand),
                                                  index: request.PageRequest.Page,
                                                  size: request.PageRequest.PageSize
                                              );
            var mappedCarDamageListModel = _mapper.Map<GetListResponse<GetListByCarIdCarDamageListItemDto>>(carDamages);
            return mappedCarDamageListModel;
        }
    }
}