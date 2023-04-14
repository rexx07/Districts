using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.CorporateCustomers.Commands.Delete;

public class DeletedCorporateCustomerResponse : IDto
{
    public int Id { get; set; }
}