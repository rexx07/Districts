using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.CorporateCustomers.Queries.GetByCustomerId;
using Modules.BaseApplication.Features.IndividualCustomers.Commands.Create;
using Modules.BaseApplication.Features.IndividualCustomers.Commands.Delete;
using Modules.BaseApplication.Features.IndividualCustomers.Commands.Update;
using Modules.BaseApplication.Features.IndividualCustomers.Queries.GetById;
using Modules.BaseApplication.Features.IndividualCustomers.Queries.GetList;

namespace Modules.BaseApplication.Features.IndividualCustomers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IndividualCustomer, CreateIndividualCustomerCommand>().ReverseMap();
        CreateMap<IndividualCustomer, CreatedIndividualCustomerResponse>().ReverseMap();
        CreateMap<IndividualCustomer, UpdateIndividualCustomerCommand>().ReverseMap();
        CreateMap<IndividualCustomer, UpdatedIndividualCustomerResponse>().ReverseMap();
        CreateMap<IndividualCustomer, DeleteIndividualCustomerCommand>().ReverseMap();
        CreateMap<IndividualCustomer, DeletedIndividualCustomerResponse>().ReverseMap();
        CreateMap<IndividualCustomer, GetByIdIndividualCustomerResponse>().ReverseMap();
        CreateMap<IndividualCustomer, GetByCustomerIdCorporateCustomerResponse>().ReverseMap();
        CreateMap<IndividualCustomer, GetListIndividualCustomerListItemDto>().ReverseMap();
        CreateMap<IPaginate<IndividualCustomer>, GetListResponse<GetListIndividualCustomerListItemDto>>().ReverseMap();
    }
}