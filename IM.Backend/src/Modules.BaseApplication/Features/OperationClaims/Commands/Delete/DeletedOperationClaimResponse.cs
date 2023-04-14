using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.OperationClaims.Commands.Delete;

public class DeletedOperationClaimResponse : IDto
{
    public int Id { get; set; }
}