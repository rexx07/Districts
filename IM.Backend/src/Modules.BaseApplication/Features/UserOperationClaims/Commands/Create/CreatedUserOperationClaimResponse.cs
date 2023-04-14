using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.UserOperationClaims.Commands.Create;

public class CreatedUserOperationClaimResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}