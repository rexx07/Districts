using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using MediatR;

namespace Application.Features.CarDamages.Queries.GetList;

public class GetListCarDamageQuery : IRequest<GetListResponse<GetListCarDamageListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListCarDamageQueryHandler : IRequestHandler<GetListCarDamageQuery,
            GetListResponse<GetListCarDamageListItemDto>>
    {
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;

        public GetListCarDamageQueryHandler(ICarDamageRepository carDamageRepository, IMapper mapper)
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCarDamageListItemDto>> Handle(
            GetListCarDamageQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<CarDamage> carDamages = await _carDamageRepository.GetListAsync(
                                                  include: c =>
                                                      c.Include(cd => cd.Car).ThenInclude(c => c.Model)
                                                       .ThenInclude(m => m.Brand),
                                                  index: request.PageRequest.Page,
                                                  size: request.PageRequest.PageSize
                                              );
            var mappedCarDamageListModel = _mapper.Map<GetListResponse<GetListCarDamageListItemDto>>(carDamages);
            return mappedCarDamageListModel;
        }
    }
}