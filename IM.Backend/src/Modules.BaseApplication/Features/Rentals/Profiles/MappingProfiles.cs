using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.Rentals.Commands.Create;
using Modules.BaseApplication.Features.Rentals.Commands.Delete;
using Modules.BaseApplication.Features.Rentals.Commands.PickUp;
using Modules.BaseApplication.Features.Rentals.Commands.Update;
using Modules.BaseApplication.Features.Rentals.Queries.GetById;
using Modules.BaseApplication.Features.Rentals.Queries.GetList;

namespace Modules.BaseApplication.Features.Rentals.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Rental, CreateRentalCommand>().ReverseMap();
        CreateMap<Rental, CreatedRentalResponse>().ReverseMap();
        CreateMap<Rental, UpdateRentalCommand>().ReverseMap();
        CreateMap<Rental, PickUpRentalResponse>().ReverseMap();
        CreateMap<Rental, DeleteRentalCommand>().ReverseMap();
        CreateMap<Rental, DeletedRentalResponse>().ReverseMap();
        CreateMap<Rental, GetByIdRentalResponse>();
        CreateMap<Rental, GetListRentalListItemDto>()
            .ForMember(destinationMember: r => r.CarModelBrandName,
                       memberOptions: opt => opt.MapFrom(r => r.Vehicle.Model.Brand.Name))
            .ForMember(destinationMember: r => r.CarModelName, memberOptions: opt => opt.MapFrom(r => r.Vehicle.Model.Name))
            .ForMember(
                destinationMember: r => r.CustomerFullName,
                memberOptions: opt =>
                    opt.MapFrom(
                        r =>
                            r.Customer.IndividualCustomer != null
                                ? $"{r.Customer.IndividualCustomer.FirstName} {r.Customer.IndividualCustomer.FirstName}"
                                : r.Customer.CorporateCustomer.CompanyName
                    )
            )
            .ReverseMap();
        CreateMap<IPaginate<Rental>, GetListResponse<GetListRentalListItemDto>>().ReverseMap();
    }
}