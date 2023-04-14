using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.IndividualCustomers.Queries.GetList;

public class GetListIndividualCustomerListItemDto : IDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }
}