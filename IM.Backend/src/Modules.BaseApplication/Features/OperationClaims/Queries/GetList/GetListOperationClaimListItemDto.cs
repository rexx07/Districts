using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}