using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Commands.Create;

public class CreatedFindeksCreditRateResponse : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}