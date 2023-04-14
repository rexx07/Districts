using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.CarDamages.Commands.Delete;

public class DeletedCarDamageResponse : IDto
{
    public int Id { get; set; }
}