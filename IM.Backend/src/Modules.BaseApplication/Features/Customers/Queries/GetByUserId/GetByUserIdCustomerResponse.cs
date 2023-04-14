using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Customers.Queries.GetByUserId;

public class GetByUserIdCustomerResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}