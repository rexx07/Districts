using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.Models.Commands.Create;
using Modules.BaseApplication.Features.Models.Commands.Delete;
using Modules.BaseApplication.Features.Models.Commands.Update;
using Modules.BaseApplication.Features.Models.Queries.GetById;
using Modules.BaseApplication.Features.Models.Queries.GetList;
using Modules.BaseApplication.Features.Models.Queries.GetListByDynamic;

namespace Modules.BaseApplication.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Model, CreateModelCommand>().ReverseMap();
        CreateMap<Model, CreatedModelResponse>().ReverseMap();
        CreateMap<Model, UpdateModelCommand>().ReverseMap();
        CreateMap<Model, UpdatedModelResponse>().ReverseMap();
        CreateMap<Model, DeleteModelCommand>().ReverseMap();
        CreateMap<Model, DeletedModelResponse>().ReverseMap();
        CreateMap<Model, GetByIdModelResponse>().ReverseMap();
        CreateMap<Model, GetListModelListItemDto>()
            .ForMember(destinationMember: c => c.BrandName, memberOptions: opt => opt.MapFrom(c => c.Brand.Name))
            .ForMember(destinationMember: c => c.FuelName, memberOptions: opt => opt.MapFrom(c => c.Fuel.Name))
            .ForMember(destinationMember: c => c.TransmissionName,
                       memberOptions: opt => opt.MapFrom(c => c.Transmission.Name));
        CreateMap<IPaginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
        CreateMap<Model, GetListByDynamicModelListItemDto>()
            .ForMember(destinationMember: c => c.BrandName, memberOptions: opt => opt.MapFrom(c => c.Brand.Name))
            .ForMember(destinationMember: c => c.FuelName, memberOptions: opt => opt.MapFrom(c => c.Fuel.Name))
            .ForMember(destinationMember: c => c.TransmissionName,
                       memberOptions: opt => opt.MapFrom(c => c.Transmission.Name));
        CreateMap<IPaginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();
    }
}