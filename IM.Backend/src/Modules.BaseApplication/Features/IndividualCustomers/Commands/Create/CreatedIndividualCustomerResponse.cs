using Application.Dtos;

namespace Application.Features.IndividualCustomers.Commands.Create;

public class CreatedIndividualCustomerResponse : IDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }
}