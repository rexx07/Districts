using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.AdditionalServices.Commands.Update;

public class UpdatedAdditionalServiceResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
}