using System.Reflection;
using Core.Infrastructure.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Modules.BaseApplication;
using Modules.BaseApplication.Pipelines.Authorization;
using Modules.BaseApplication.Pipelines.Caching;
using Modules.BaseApplication.Pipelines.Logging;
using Modules.BaseApplication.Pipelines.Transaction;
using Modules.BaseApplication.Pipelines.Validation;

namespace Modules.AirTransport;

public static class Startup
{
    public static IServiceCollection AddAirTransportModule(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(assembly: Assembly.GetExecutingAssembly(), type: typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}