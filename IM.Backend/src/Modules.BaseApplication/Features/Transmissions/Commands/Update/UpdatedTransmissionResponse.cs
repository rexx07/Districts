using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Transmissions.Commands.Update;

public class UpdatedTransmissionResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}