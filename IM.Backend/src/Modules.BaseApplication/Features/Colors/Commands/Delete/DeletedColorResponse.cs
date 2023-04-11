using Core.Infrastructure.Dtos;

namespace Application.Features.Colors.Commands.Delete;

public class DeletedColorResponse : IDto
{
    public int Id { get; set; }
}