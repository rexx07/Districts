using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}