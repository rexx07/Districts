using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.OperationClaims.Commands.Update;

public class UpdatedOperationClaimResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}