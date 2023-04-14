using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Colors.Commands.Update;

public class UpdatedColorResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}