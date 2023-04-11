using Core.Infrastructure.Dtos;

namespace Application.Features.FindeksCreditRates.Commands.UpdateFromService;

public class UpdateFindeksCreditRateFromServiceResponse : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}