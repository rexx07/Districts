using Core.Domain.Enums;
using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.RentalBranches.Queries.GetById;

public class GetByIdRentalBranchResponse : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}