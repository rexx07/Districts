using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.RentalBranches.Commands.Delete;

public class DeletedRentalBranchResponse : IDto
{
    public int Id { get; set; }
}