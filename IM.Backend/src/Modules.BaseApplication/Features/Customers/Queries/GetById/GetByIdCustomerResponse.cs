using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Customers.Queries.GetById;

public class GetByIdCustomerResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}