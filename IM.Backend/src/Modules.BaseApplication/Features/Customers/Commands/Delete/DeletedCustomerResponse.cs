using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Customers.Commands.Delete;

public class DeletedCustomerResponse : IDto
{
    public int Id { get; set; }
}