using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.CorporateCustomers.Commands.Create;
using Modules.BaseApplication.Features.CorporateCustomers.Commands.Delete;
using Modules.BaseApplication.Features.CorporateCustomers.Commands.Update;
using Modules.BaseApplication.Features.CorporateCustomers.Queries.GetByCustomerId;
using Modules.BaseApplication.Features.CorporateCustomers.Queries.GetById;
using Modules.BaseApplication.Features.CorporateCustomers.Queries.GetList;

namespace Modules.BaseApplication.Features.CorporateCustomers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CorporateCustomer, CreateCorporateCustomerCommand>().ReverseMap();
        CreateMap<CorporateCustomer, CreatedCorporateCustomerResponse>().ReverseMap();
        CreateMap<CorporateCustomer, UpdateCorporateCustomerCommand>().ReverseMap();
        CreateMap<CorporateCustomer, UpdatedCorporateCustomerResponse>().ReverseMap();
        CreateMap<CorporateCustomer, DeleteCorporateCustomerCommand>().ReverseMap();
        CreateMap<CorporateCustomer, DeletedCorporateCustomerResponse>().ReverseMap();
        CreateMap<CorporateCustomer, GetByIdCorporateCustomerResponse>().ReverseMap();
        CreateMap<CorporateCustomer, GetByCustomerIdCorporateCustomerResponse>().ReverseMap();
        CreateMap<CorporateCustomer, GetListCorporateCustomerListItemDto>().ReverseMap();
        CreateMap<IPaginate<CorporateCustomer>, GetListResponse<GetListCorporateCustomerListItemDto>>().ReverseMap();
    }
}