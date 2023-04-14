using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Colors.Commands.Delete;

public class DeletedColorResponse : IDto
{
    public int Id { get; set; }
}