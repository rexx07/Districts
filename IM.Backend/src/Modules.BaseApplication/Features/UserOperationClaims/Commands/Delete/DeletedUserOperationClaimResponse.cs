using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.UserOperationClaims.Commands.Delete;

public class DeletedUserOperationClaimResponse : IDto
{
    public int Id { get; set; }
}