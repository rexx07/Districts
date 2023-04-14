using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Commands.Update;

public class UpdatedFindeksCreditRateResponse : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}