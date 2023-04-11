using Core.Infrastructure.Dtos;

namespace Application.Features.Customers.Commands.Update;

public class UpdatedCustomerResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}