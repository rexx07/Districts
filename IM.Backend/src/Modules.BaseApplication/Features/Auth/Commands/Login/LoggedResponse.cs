using Core.Domain.Entities.Security;
using Core.Domain.Enums;
using Core.Infrastructure.Security.JWT;
using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Auth.Commands.Login;

public class LoggedResponse : IDto
{
    public AccessToken? AccessToken { get; set; }
    public RefreshToken? RefreshToken { get; set; }
    public AuthenticatorType? RequiredAuthenticatorType { get; set; }

    public LoggedHttpResponse ToHttpResponse() =>
        new() { AccessToken = AccessToken, RequiredAuthenticatorType = RequiredAuthenticatorType };

    public class LoggedHttpResponse
    {
        public AccessToken? AccessToken { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }
    }
}