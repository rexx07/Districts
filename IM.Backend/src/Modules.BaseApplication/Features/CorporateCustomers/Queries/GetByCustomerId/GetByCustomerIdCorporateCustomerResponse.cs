using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.CorporateCustomers.Queries.GetByCustomerId;

public class GetByCustomerIdCorporateCustomerResponse : IDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }
}