using Core.Infrastructure.Security.JWT;
using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Users.Commands.UpdateFromAuth;

public class UpdatedUserFromAuthResponse : IDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public AccessToken AccessToken { get; set; }
}