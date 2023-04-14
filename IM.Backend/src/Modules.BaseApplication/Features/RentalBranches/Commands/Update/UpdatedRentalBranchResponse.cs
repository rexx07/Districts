using Core.Domain.Enums;
using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.RentalBranches.Commands.Update;

public class UpdatedRentalBranchResponse : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}