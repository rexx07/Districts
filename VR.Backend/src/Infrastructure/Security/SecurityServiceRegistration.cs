using Infrastructure.Security.EmailAuthenticator;
using Infrastructure.Security.JWT;
using Infrastructure.Security.OtpAuthenticator;
using Infrastructure.Security.OtpAuthenticator.OtpNet;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Security;

public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHelper, JwtHelper>();
        services.AddScoped<IEmailAuthenticatorHelper, EmailAuthenticatorHelper>();
        services.AddScoped<IOtpAuthenticatorHelper, OtpNetOtpAuthenticatorHelper>();
        return services;
    }
}