using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Transmissions.Queries.GetById;

public class GetByIdTransmissionResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}