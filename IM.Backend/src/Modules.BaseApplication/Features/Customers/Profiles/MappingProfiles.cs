using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.Customers.Commands.Create;
using Modules.BaseApplication.Features.Customers.Commands.Delete;
using Modules.BaseApplication.Features.Customers.Commands.Update;
using Modules.BaseApplication.Features.Customers.Queries.GetById;
using Modules.BaseApplication.Features.Customers.Queries.GetByUserId;
using Modules.BaseApplication.Features.Customers.Queries.GetList;

namespace Modules.BaseApplication.Features.Customers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
        CreateMap<Customer, CreatedCustomerResponse>().ReverseMap();
        CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
        CreateMap<Customer, UpdatedCustomerResponse>().ReverseMap();
        CreateMap<Customer, DeleteCustomerCommand>().ReverseMap();
        CreateMap<Customer, DeletedCustomerResponse>().ReverseMap();
        CreateMap<Customer, GetByIdCustomerResponse>().ReverseMap();
        CreateMap<Customer, GetByUserIdCustomerQuery>().ReverseMap();
        CreateMap<Customer, GetListCustomerListItemDto>().ReverseMap();
        CreateMap<IPaginate<Customer>, GetListResponse<GetListCustomerListItemDto>>().ReverseMap();
    }
}