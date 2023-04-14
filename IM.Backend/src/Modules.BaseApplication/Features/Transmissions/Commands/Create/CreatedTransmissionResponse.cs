using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Transmissions.Commands.Create;

public class CreatedTransmissionResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}