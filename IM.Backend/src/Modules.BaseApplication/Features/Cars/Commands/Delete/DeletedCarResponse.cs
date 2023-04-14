using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Cars.Commands.Delete;

public class DeletedCarResponse : IDto
{
    public int Id { get; set; }
}