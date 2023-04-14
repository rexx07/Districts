using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;

public class GetByIdFindeksCreditRateResponse : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}