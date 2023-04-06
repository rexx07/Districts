using Application.Requests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Paging;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;

namespace Application.Features.Fuels.Queries.GetList;

public class GetListFuelQuery : IRequest<GetListResponse<GetListFuelListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFuelQueryHandler : IRequestHandler<GetListFuelQuery, GetListResponse<GetListFuelListItemDto>>
    {
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;

        public GetListFuelQueryHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFuelListItemDto>> Handle(
            GetListFuelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Fuel> fuels =
                await _fuelRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            var mappedFuelListModel = _mapper.Map<GetListResponse<GetListFuelListItemDto>>(fuels);
            return mappedFuelListModel;
        }
    }
}