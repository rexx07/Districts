using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Paging;
using Modules.BaseApplication.Features.Invoices.Commands.Create;
using Modules.BaseApplication.Features.Invoices.Commands.Delete;
using Modules.BaseApplication.Features.Invoices.Commands.Update;
using Modules.BaseApplication.Features.Invoices.Queries.GetList;
using Modules.BaseApplication.Features.Invoices.Queries.GetListByCustomer;
using Modules.BaseApplication.Features.Invoices.Queries.GetListByDates;

namespace Modules.BaseApplication.Features.Invoices.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Invoice, CreateInvoiceCommand>().ReverseMap();
        CreateMap<Invoice, CreatedInvoiceResponse>().ReverseMap();
        CreateMap<Invoice, UpdateInvoiceCommand>().ReverseMap();
        CreateMap<Invoice, UpdatedInvoiceResponse>().ReverseMap();
        CreateMap<Invoice, DeleteInvoiceCommand>().ReverseMap();
        CreateMap<Invoice, DeletedInvoiceResponse>().ReverseMap();
        CreateMap<Invoice, GetListInvoiceListItemDto>()
            .ForMember(
                destinationMember: i => i.CustomerName,
                memberOptions: opt =>
                    opt.MapFrom(
                        i =>
                            i.Customer.IndividualCustomer != null
                                ? $"{i.Customer.IndividualCustomer.FirstName} {i.Customer.IndividualCustomer.LastName}"
                                : i.Customer.CorporateCustomer.CompanyName
                    )
            )
            .ReverseMap();
        CreateMap<IPaginate<Invoice>, GetListResponse<GetListInvoiceListItemDto>>().ReverseMap();
        CreateMap<Invoice, GetListByCustomerInvoiceListItemDto>()
            .ForMember(
                destinationMember: i => i.CustomerName,
                memberOptions: opt =>
                    opt.MapFrom(
                        i =>
                            i.Customer.IndividualCustomer != null
                                ? $"{i.Customer.IndividualCustomer.FirstName} {i.Customer.IndividualCustomer.LastName}"
                                : i.Customer.CorporateCustomer.CompanyName
                    )
            )
            .ReverseMap();
        CreateMap<IPaginate<Invoice>, GetListResponse<GetListByCustomerInvoiceListItemDto>>().ReverseMap();
        CreateMap<Invoice, GetListByDatesInvoiceListItemDto>()
            .ForMember(
                destinationMember: i => i.CustomerName,
                memberOptions: opt =>
                    opt.MapFrom(
                        i =>
                            i.Customer.IndividualCustomer != null
                                ? $"{i.Customer.IndividualCustomer.FirstName} {i.Customer.IndividualCustomer.LastName}"
                                : i.Customer.CorporateCustomer.CompanyName
                    )
            )
            .ReverseMap();
        CreateMap<IPaginate<Invoice>, GetListResponse<GetListByDatesInvoiceListItemDto>>().ReverseMap();
    }
}