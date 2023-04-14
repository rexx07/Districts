using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.Colors.Commands.Create;
using Modules.BaseApplication.Features.Colors.Commands.Delete;
using Modules.BaseApplication.Features.Colors.Commands.Update;
using Modules.BaseApplication.Features.Colors.Queries.GetById;
using Modules.BaseApplication.Features.Colors.Queries.GetList;

namespace Modules.BaseApplication.Features.Colors.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Color, CreateColorCommand>().ReverseMap();
        CreateMap<Color, CreatedColorResponse>().ReverseMap();
        CreateMap<Color, UpdateColorCommand>().ReverseMap();
        CreateMap<Color, UpdatedColorResponse>().ReverseMap();
        CreateMap<Color, DeleteColorCommand>().ReverseMap();
        CreateMap<Color, DeletedColorResponse>().ReverseMap();
        CreateMap<Color, GetByIdColorResponse>().ReverseMap();
        CreateMap<Color, GetListColorListItemDto>().ReverseMap();
        CreateMap<IPaginate<Color>, GetListResponse<GetListColorListItemDto>>().ReverseMap();
    }
}