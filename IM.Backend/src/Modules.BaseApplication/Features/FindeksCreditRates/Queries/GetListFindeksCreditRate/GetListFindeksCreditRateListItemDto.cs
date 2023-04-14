using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Queries.GetListFindeksCreditRate;

public class GetListFindeksCreditRateListItemDto : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}