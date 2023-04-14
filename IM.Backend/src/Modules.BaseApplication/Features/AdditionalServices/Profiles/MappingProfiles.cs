using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.AdditionalServices.Commands.Create;
using Modules.BaseApplication.Features.AdditionalServices.Commands.Delete;
using Modules.BaseApplication.Features.AdditionalServices.Commands.Update;
using Modules.BaseApplication.Features.AdditionalServices.Queries.GetById;
using Modules.BaseApplication.Features.AdditionalServices.Queries.GetList;

namespace Modules.BaseApplication.Features.AdditionalServices.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AdditionalService, CreateAdditionalServiceCommand>().ReverseMap();
        CreateMap<AdditionalService, CreatedAdditionalServiceResponse>().ReverseMap();
        CreateMap<AdditionalService, UpdateAdditionalServiceCommand>().ReverseMap();
        CreateMap<AdditionalService, UpdatedAdditionalServiceResponse>().ReverseMap();
        CreateMap<AdditionalService, DeleteAdditionalServiceCommand>().ReverseMap();
        CreateMap<AdditionalService, DeletedAdditionalServiceResponse>().ReverseMap();
        CreateMap<AdditionalService, GetByIdAdditionalServiceResponse>().ReverseMap();
        CreateMap<AdditionalService, GetListAdditionalServiceListItemDto>().ReverseMap();
        CreateMap<IPaginate<AdditionalService>, GetListResponse<GetListAdditionalServiceListItemDto>>().ReverseMap();
    }
}