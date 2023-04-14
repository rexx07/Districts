using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Commands.UpdateFromService;

public class UpdateFindeksCreditRateFromServiceResponse : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}