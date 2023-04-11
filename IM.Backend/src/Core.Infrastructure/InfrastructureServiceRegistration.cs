using Core.Infrastructure.Persistence;
using Core.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
                                                               IConfiguration configuration)
    {
        services.AddPersistenceServices(configuration);
        services.AddSecurityServices();
        
        return services;
    }
}