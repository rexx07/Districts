using Infrastructure.Common.Logging.Serilog;
using Infrastructure.Common.Logging.Serilog.Logger;
using Infrastructure.ElasticSearch;
using Infrastructure.Mailing;
using Infrastructure.Mailing.MailKitImplementations;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IMailService, MailKitMailService>();
        services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<IElasticSearch, ElasticSearchManager>();

        services.AddDbContext<BaseDbContext>(
            options => options.UseNpgsql(config.GetConnectionString("VehicleRentalsConnectionString"))
        );
        services.AddScoped<IAdditionalServiceRepository, AdditionalServiceRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ICarDamageRepository, CarDamageRepository>();
        services.AddScoped<IColorRepository, ColorRepository>();
        services.AddScoped<ICorporateCustomerRepository, CorporateCustomerRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        services.AddScoped<IFindeksCreditRateRepository, FindeksCreditRateRepository>();
        services.AddScoped<IFuelRepository, FuelRepository>();
        services.AddScoped<IIndividualCustomerRepository, IndividualCustomerRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IModelRepository, ModelRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRentalsAdditionalServiceRepository, RentalsAdditionalServiceRepository>();
        services.AddScoped<IRentalBranchRepository, RentalBranchRepository>();
        services.AddScoped<ITransmissionRepository, TransmissionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

        return services;
    }
}