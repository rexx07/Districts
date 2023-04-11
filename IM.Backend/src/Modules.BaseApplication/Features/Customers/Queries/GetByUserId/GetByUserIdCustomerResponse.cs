using Core.Infrastructure.Dtos;

namespace Application.Features.Customers.Queries.GetByUserId;

public class GetByUserIdCustomerResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
}