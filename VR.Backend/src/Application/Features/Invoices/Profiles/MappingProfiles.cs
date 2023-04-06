using Application.Features.Invoices.Commands.Create;
using Application.Features.Invoices.Commands.Delete;
using Application.Features.Invoices.Commands.Update;
using Application.Features.Invoices.Queries.GetList;
using Application.Features.Invoices.Queries.GetListByCustomer;
using Application.Features.Invoices.Queries.GetListByDates;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Paging;

namespace Application.Features.Invoices.Profiles;

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