using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Users.Commands.Delete;

public class DeletedUserResponse : IDto
{
    public int Id { get; set; }
}