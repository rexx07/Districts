using AutoMapper;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.Brands.Commands.Create;
using Modules.BaseApplication.Features.Brands.Commands.Delete;
using Modules.BaseApplication.Features.Brands.Commands.Update;
using Modules.BaseApplication.Features.Brands.Queries.GetById;
using Modules.BaseApplication.Features.Brands.Queries.GetList;

namespace Modules.BaseApplication.Features.Brands.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Brand, CreateBrandCommand>().ReverseMap();
        CreateMap<Brand, CreatedBrandResponse>().ReverseMap();
        CreateMap<Brand, UpdateBrandCommand>().ReverseMap();
        CreateMap<Brand, UpdatedBrandResponse>().ReverseMap();
        CreateMap<Brand, DeleteBrandCommand>().ReverseMap();
        CreateMap<Brand, DeletedBrandResponse>().ReverseMap();
        CreateMap<Brand, GetByIdBrandResponse>().ReverseMap();
        CreateMap<Brand, GetListBrandListItemDto>().ReverseMap();
        CreateMap<IPaginate<Brand>, GetListResponse<GetListBrandListItemDto>>().ReverseMap();
    }
}