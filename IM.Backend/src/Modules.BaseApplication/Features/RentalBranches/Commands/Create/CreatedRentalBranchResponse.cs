using Core.Domain.Enums;
using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.RentalBranches.Commands.Create;

public class CreatedRentalBranchResponse : IDto
{
    public int Id { get; set; }
    public City City { get; set; }
}