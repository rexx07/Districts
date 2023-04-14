using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Queries.GetByCustomerIdFindeksCreditRate;

public class GetByCustomerIdFindeksCreditRateResponse : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}