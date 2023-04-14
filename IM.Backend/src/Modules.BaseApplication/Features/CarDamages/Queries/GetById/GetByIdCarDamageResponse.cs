using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.CarDamages.Queries.GetById;

public class GetByIdCarDamageResponse : IDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }
}