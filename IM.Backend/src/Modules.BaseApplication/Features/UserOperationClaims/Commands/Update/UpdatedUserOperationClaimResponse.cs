using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.UserOperationClaims.Commands.Update;

public class UpdatedUserOperationClaimResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}