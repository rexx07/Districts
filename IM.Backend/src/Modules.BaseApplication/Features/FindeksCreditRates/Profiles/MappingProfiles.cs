using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.CorporateCustomers.Queries.GetByCustomerId;
using Modules.BaseApplication.Features.FindeksCreditRates.Commands.Create;
using Modules.BaseApplication.Features.FindeksCreditRates.Commands.Delete;
using Modules.BaseApplication.Features.FindeksCreditRates.Commands.Update;
using Modules.BaseApplication.Features.FindeksCreditRates.Commands.UpdateByUserIdFromService;
using Modules.BaseApplication.Features.FindeksCreditRates.Commands.UpdateFromService;
using Modules.BaseApplication.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;
using Modules.BaseApplication.Features.FindeksCreditRates.Queries.GetListFindeksCreditRate;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<FindeksCreditRate, CreateFindeksCreditRateCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, CreatedFindeksCreditRateResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateFindeksCreditRateCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdatedFindeksCreditRateResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateFindeksCreditRateFromServiceCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateFindeksCreditRateFromServiceResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateByUserIdFindeksCreditRateFromServiceCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateByUserIdFindeksCreditRateFromServiceResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, DeleteFindeksCreditRateCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, DeletedFindeksCreditRateResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, GetByIdFindeksCreditRateResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, GetByCustomerIdCorporateCustomerResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, GetListFindeksCreditRateListItemDto>().ReverseMap();
        CreateMap<IPaginate<FindeksCreditRate>, GetListResponse<GetListFindeksCreditRateListItemDto>>().ReverseMap();
    }
}