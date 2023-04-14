using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Transmissions.Queries.GetList;

public class GetListTransmissionListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}