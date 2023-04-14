using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.IndividualCustomers.Commands.Delete;

public class DeletedIndividualCustomerResponse : IDto
{
    public int Id { get; set; }
}