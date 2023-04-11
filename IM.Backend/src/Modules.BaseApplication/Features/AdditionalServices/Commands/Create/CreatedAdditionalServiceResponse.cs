using Core.Infrastructure.Dtos;

namespace Application.Features.AdditionalServices.Commands.Create;

public class CreatedAdditionalServiceResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
}