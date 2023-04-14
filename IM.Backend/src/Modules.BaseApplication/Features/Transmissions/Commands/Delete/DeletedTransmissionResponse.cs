using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Transmissions.Commands.Delete;

public class DeletedTransmissionResponse : IDto
{
    public int Id { get; set; }
}