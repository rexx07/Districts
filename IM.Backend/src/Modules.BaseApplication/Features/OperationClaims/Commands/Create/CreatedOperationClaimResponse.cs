using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.OperationClaims.Commands.Create;

public class CreatedOperationClaimResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}