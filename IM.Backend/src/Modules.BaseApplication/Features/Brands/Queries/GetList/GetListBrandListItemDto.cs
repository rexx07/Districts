using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Brands.Queries.GetList;

public class GetListBrandListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}