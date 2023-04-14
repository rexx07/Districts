using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Customers.Commands.Create;

public class CreatedCustomerResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}