using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.AdditionalServices.Commands.Create;

public class CreatedAdditionalServiceResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
}