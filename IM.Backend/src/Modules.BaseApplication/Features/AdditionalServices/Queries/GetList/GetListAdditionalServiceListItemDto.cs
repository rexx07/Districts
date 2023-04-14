using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.AdditionalServices.Queries.GetList;

public class GetListAdditionalServiceListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
}