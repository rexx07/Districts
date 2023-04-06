using Application.Dtos;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Security.JWT;

namespace Application.Features.Auth.Commands.Login;

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