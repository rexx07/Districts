using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Models.Commands.Delete;

public class DeletedModelResponse : IDto
{
    public int Id { get; set; }
}