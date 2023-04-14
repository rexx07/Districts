using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Auth.Commands.EnableOtpAuthenticator;

public class EnabledOtpAuthenticatorResponse : IDto
{
    public string SecretKey { get; set; }
}