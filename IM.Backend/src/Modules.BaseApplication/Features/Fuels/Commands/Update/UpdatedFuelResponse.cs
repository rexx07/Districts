using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Fuels.Commands.Update;

public class UpdatedFuelResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}