using Core.Domain.Entities.Security;
using Core.Infrastructure.Security.JWT;

namespace Modules.BaseApplication.Features.Auth.Commands.Register;

public class RegisteredResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}