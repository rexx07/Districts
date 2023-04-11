using System.Reflection;
using Application.Adapters.FakeFindeksService;
using Application.Adapters.FakePOSService;
using Application.Adapters.ImageService;
using Application.Pipelines.Authorization;
using Application.Pipelines.Caching;
using Application.Pipelines.Logging;
using Application.Pipelines.Transaction;
using Application.Pipelines.Validation;
using Application.Services.AdditionalServiceService;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.CarService;
using Application.Services.CustomerService;
using Application.Services.FindeksCreditRateService;
using Application.Services.FindeksService;
using Application.Services.ImageService;
using Application.Services.InvoiceService;
using Application.Services.ModelService;
using Application.Services.POSService;
using Application.Services.RentalService;
using Application.Services.RentalsIAdditionalServiceService;
using Application.Services.UserService;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.Infrastructure.ElasticSearch;
using Core.Infrastructure.Mailing;
using Core.Infrastructure.Mailing.MailKitImplementations;
using Core.Infrastructure.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

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