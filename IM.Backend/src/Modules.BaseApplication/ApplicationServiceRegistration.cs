using System.Reflection;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.Infrastructure.ElasticSearch;
using Core.Infrastructure.Mailing;
using Core.Infrastructure.Mailing.MailKitImplementations;
using Core.Infrastructure.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Modules.BaseApplication.Adapters.FakeFindeksService;
using Modules.BaseApplication.Adapters.FakePOSService;
using Modules.BaseApplication.Adapters.ImageService;
using Modules.BaseApplication.Pipelines.Authorization;
using Modules.BaseApplication.Pipelines.Caching;
using Modules.BaseApplication.Pipelines.Logging;
using Modules.BaseApplication.Pipelines.Transaction;
using Modules.BaseApplication.Pipelines.Validation;
using Modules.BaseApplication.Services.AdditionalServiceService;
using Modules.BaseApplication.Services.AuthenticatorService;
using Modules.BaseApplication.Services.AuthService;
using Modules.BaseApplication.Services.CarService;
using Modules.BaseApplication.Services.CustomerService;
using Modules.BaseApplication.Services.FindeksCreditRateService;
using Modules.BaseApplication.Services.FindeksService;
using Modules.BaseApplication.Services.ImageService;
using Modules.BaseApplication.Services.InvoiceService;
using Modules.BaseApplication.Services.ModelService;
using Modules.BaseApplication.Services.POSService;
using Modules.BaseApplication.Services.RentalService;
using Modules.BaseApplication.Services.RentalsIAdditionalServiceService;
using Modules.BaseApplication.Services.UserService;

namespace Modules.BaseApplication;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
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

        services.AddScoped<IAdditionalServiceService, AdditionalServiceManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<ICarService, CarManager>();
        services.AddScoped<ICustomerService, CustomerManager>();
        services.AddScoped<IFindeksCreditRateService, FindeksCreditRateManager>();
        services.AddScoped<IInvoiceService, InvoiceManager>();
        services.AddScoped<IModelService, ModelManager>();
        services.AddScoped<IRentalService, RentalManager>();
        services.AddScoped<IRentalsAdditionalServiceService, RentalsAdditionalServiceManager>();
        services.AddScoped<IUserService, UserManager>();

        services.AddSingleton<IMailService, MailKitMailService>();
        services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<IElasticSearch, ElasticSearchManager>();

        services.AddScoped<IFindeksService, FakeFindeksServiceAdapter>();
        services.AddScoped<IPOSService, FakePOSServiceAdapter>();
        services.AddScoped<ImageServiceBase, CloudinaryImageServiceAdapter>();

        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}