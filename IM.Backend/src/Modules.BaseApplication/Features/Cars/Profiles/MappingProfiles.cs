using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.Cars.Commands.Create;
using Modules.BaseApplication.Features.Cars.Commands.Delete;
using Modules.BaseApplication.Features.Cars.Commands.DeliverRental;
using Modules.BaseApplication.Features.Cars.Commands.Maintain;
using Modules.BaseApplication.Features.Cars.Commands.Update;
using Modules.BaseApplication.Features.Cars.Queries.GetById;
using Modules.BaseApplication.Features.Cars.Queries.GetList;
using Modules.BaseApplication.Features.Cars.Queries.GetListByDynamic;

namespace Modules.BaseApplication.Features.Cars.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Vehicle, CreateCarCommand>().ReverseMap();
        CreateMap<Vehicle, CreatedCarResponse>().ReverseMap();
        CreateMap<Vehicle, UpdateCarCommand>().ReverseMap();
        CreateMap<Vehicle, UpdatedCarResponse>().ReverseMap();
        CreateMap<Vehicle, DeliveredCarResponse>().ReverseMap();
        CreateMap<Vehicle, MaintainedCarResponse>().ReverseMap();
        CreateMap<Vehicle, DeleteCarCommand>().ReverseMap();
        CreateMap<Vehicle, DeletedCarResponse>().ReverseMap();
        CreateMap<Vehicle, GetByIdCarResponse>().ReverseMap();
        CreateMap<Vehicle, GetListCarListItemDto>()
            .ForMember(destinationMember: c => c.ColorName, memberOptions: opt => opt.MapFrom(c => c.Color.Name))
            .ForMember(destinationMember: c => c.ModelName, memberOptions: opt => opt.MapFrom(c => c.Model.Name))
            .ForMember(destinationMember: c => c.BrandName, memberOptions: opt => opt.MapFrom(c => c.Model.Brand.Name));
        CreateMap<IPaginate<Vehicle>, GetListResponse<GetListCarListItemDto>>().ReverseMap();
        CreateMap<Vehicle, GetListByDynamicCarListItemDto>()
            .ForMember(destinationMember: c => c.ColorName, memberOptions: opt => opt.MapFrom(c => c.Color.Name))
            .ForMember(destinationMember: c => c.ModelName, memberOptions: opt => opt.MapFrom(c => c.Model.Name))
            .ForMember(destinationMember: c => c.BrandName, memberOptions: opt => opt.MapFrom(c => c.Model.Brand.Name));
        CreateMap<IPaginate<Vehicle>, GetListResponse<GetListByDynamicCarListItemDto>>().ReverseMap();
    }
}