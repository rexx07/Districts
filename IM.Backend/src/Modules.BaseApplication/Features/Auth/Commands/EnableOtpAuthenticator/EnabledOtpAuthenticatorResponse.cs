using Core.Infrastructure.Dtos;

namespace Application.Features.Auth.Commands.EnableOtpAuthenticator;

public class EnabledOtpAuthenticatorResponse : IDto
{
    public string SecretKey { get; set; }
}