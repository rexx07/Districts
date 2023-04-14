using Core.Domain.Entities.Security;
using Core.Infrastructure.Security.JWT;
using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Auth.Commands.RefleshToken;

public class RefreshedTokensResponse : IDto
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}