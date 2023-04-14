using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.Fuels.Commands.Create;
using Modules.BaseApplication.Features.Fuels.Commands.Delete;
using Modules.BaseApplication.Features.Fuels.Commands.Update;
using Modules.BaseApplication.Features.Fuels.Queries.GetById;
using Modules.BaseApplication.Features.Fuels.Queries.GetList;

namespace Modules.BaseApplication.Features.Fuels.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Fuel, CreateFuelCommand>().ReverseMap();
        CreateMap<Fuel, CreatedFuelResponse>().ReverseMap();
        CreateMap<Fuel, UpdateFuelCommand>().ReverseMap();
        CreateMap<Fuel, UpdatedFuelResponse>().ReverseMap();
        CreateMap<Fuel, DeleteFuelCommand>().ReverseMap();
        CreateMap<Fuel, DeletedFuelResponse>().ReverseMap();
        CreateMap<Fuel, GetByIdFuelResponse>().ReverseMap();
        CreateMap<Fuel, GetListFuelListItemDto>().ReverseMap();
        CreateMap<IPaginate<Fuel>, GetListResponse<GetListFuelListItemDto>>().ReverseMap();
    }
}