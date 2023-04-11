using Core.Infrastructure.Dtos;

namespace Application.Features.CorporateCustomers.Queries.GetById;

public class GetByIdCorporateCustomerResponse : IDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }
}