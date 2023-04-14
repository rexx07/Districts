using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Customers.Queries.GetList;

public class GetListCustomerListItemDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}