using Core.Infrastructure.Dtos;

namespace Application.Features.Transmissions.Commands.Delete;

public class DeletedTransmissionResponse : IDto
{
    public int Id { get; set; }
}