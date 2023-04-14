using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.OperationClaims.Queries.GetById;

public class GetByIdOperationClaimResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}