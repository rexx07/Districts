using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.CorporateCustomers.Queries.GetById;

public class GetByIdCorporateCustomerResponse : IDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }
}