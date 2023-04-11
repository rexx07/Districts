using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Core.Infrastructure.Requests;
using MediatR;

namespace Application.Features.Rentals.Queries.GetList;

public class GetListRentalQuery : IRequest<GetListResponse<GetListRentalListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListRentalQueryHandler : IRequestHandler<GetListRentalQuery, GetListResponse<GetListRentalListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRentalRepository _rentalRepository;

        public GetListRentalQueryHandler(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListRentalListItemDto>> Handle(
            GetListRentalQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Rental> rentals = await _rentalRepository.GetListAsync(
                                            include: r =>
                                                r.Include(r => r.Car)
                                                 .Include(r => r.Car.Model)
                                                 .Include(r => r.Car.Model.Brand)
                                                 .Include(r => r.Car.Color)
                                                 .Include(r => r.Customer)
                                                 .Include(r => r.Customer.IndividualCustomer)
                                                 .Include(r => r.Customer.CorporateCustomer),
                                            index: request.PageRequest.Page,
                                            size: request.PageRequest.PageSize
                                        );
            var mappedRentalListModel = _mapper.Map<GetListResponse<GetListRentalListItemDto>>(rentals);
            return mappedRentalListModel;
        }
    }
}