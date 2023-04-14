using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Colors.Commands.Create;

public class CreatedColorResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}