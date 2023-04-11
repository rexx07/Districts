using Core.Infrastructure.Dtos;

namespace Application.Features.CorporateCustomers.Commands.Delete;

public class DeletedCorporateCustomerResponse : IDto
{
    public int Id { get; set; }
}