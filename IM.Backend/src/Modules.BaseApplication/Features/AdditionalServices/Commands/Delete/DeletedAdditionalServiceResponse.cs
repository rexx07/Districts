using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.AdditionalServices.Commands.Delete;

public class DeletedAdditionalServiceResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
}