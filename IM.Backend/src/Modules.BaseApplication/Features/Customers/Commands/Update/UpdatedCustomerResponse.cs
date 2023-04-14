using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Customers.Commands.Update;

public class UpdatedCustomerResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}