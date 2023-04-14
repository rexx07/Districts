using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Fuels.Commands.Delete;

public class DeletedFuelResponse : IDto
{
    public int Id { get; set; }
}