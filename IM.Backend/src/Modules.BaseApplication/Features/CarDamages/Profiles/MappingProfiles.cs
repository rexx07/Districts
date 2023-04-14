using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.CarDamages.Commands.Create;
using Modules.BaseApplication.Features.CarDamages.Commands.Delete;
using Modules.BaseApplication.Features.CarDamages.Commands.Update;
using Modules.BaseApplication.Features.CarDamages.Queries.GetById;
using Modules.BaseApplication.Features.CarDamages.Queries.GetList;
using Modules.BaseApplication.Features.CarDamages.Queries.GetListByCarId;

namespace Modules.BaseApplication.Features.CarDamages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<VehicleDamage, CreateCarDamageCommand>().ReverseMap();
        CreateMap<VehicleDamage, CreatedCarDamageResponse>().ReverseMap();
        CreateMap<VehicleDamage, UpdateCarDamageCommand>().ReverseMap();
        CreateMap<VehicleDamage, UpdatedCarDamageResponse>().ReverseMap();
        CreateMap<VehicleDamage, DeleteCarDamageCommand>().ReverseMap();
        CreateMap<VehicleDamage, DeletedCarDamageResponse>().ReverseMap();
        CreateMap<VehicleDamage, GetByIdCarDamageResponse>().ReverseMap();
        CreateMap<VehicleDamage, GetListCarDamageListItemDto>()
            .ForMember(destinationMember: c => c.CarModelBrandName,
                       memberOptions: opt => opt.MapFrom(c => c.Car.Model.Brand.Name))
            .ForMember(destinationMember: c => c.CarModelName, memberOptions: opt => opt.MapFrom(c => c.Car.Model.Name))
            .ForMember(destinationMember: c => c.CarModelYear, memberOptions: opt => opt.MapFrom(c => c.Car.ModelYear))
            .ForMember(destinationMember: c => c.CarPlate, memberOptions: opt => opt.MapFrom(c => c.Car.Plate))
            .ReverseMap();
        CreateMap<IPaginate<VehicleDamage>, GetListResponse<GetListCarDamageListItemDto>>().ReverseMap();
        CreateMap<VehicleDamage, GetListByCarIdCarDamageListItemDto>()
            .ForMember(destinationMember: c => c.CarModelBrandName,
                       memberOptions: opt => opt.MapFrom(c => c.Car.Model.Brand.Name))
            .ForMember(destinationMember: c => c.CarModelName, memberOptions: opt => opt.MapFrom(c => c.Car.Model.Name))
            .ForMember(destinationMember: c => c.CarModelYear, memberOptions: opt => opt.MapFrom(c => c.Car.ModelYear))
            .ForMember(destinationMember: c => c.CarPlate, memberOptions: opt => opt.MapFrom(c => c.Car.Plate))
            .ReverseMap();
        CreateMap<IPaginate<VehicleDamage>, GetListResponse<GetListByCarIdCarDamageListItemDto>>().ReverseMap();
    }
}