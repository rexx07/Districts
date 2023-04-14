using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.CorporateCustomers.Commands.Create;

public class CreatedCorporateCustomerResponse : IDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }
}