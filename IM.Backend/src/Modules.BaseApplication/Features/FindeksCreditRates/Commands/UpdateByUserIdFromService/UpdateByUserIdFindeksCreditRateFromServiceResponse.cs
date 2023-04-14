using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Commands.UpdateByUserIdFromService;

public class UpdateByUserIdFindeksCreditRateFromServiceResponse : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}