using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.CarDamages.Commands.Create;

public class CreatedCarDamageResponse : IDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }
}