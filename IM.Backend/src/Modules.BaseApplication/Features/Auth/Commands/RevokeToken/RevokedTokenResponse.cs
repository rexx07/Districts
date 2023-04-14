using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Auth.Commands.RevokeToken;

public class RevokedTokenResponse : IDto
{
    public int Id { get; set; }
    public string Token { get; set; }
}