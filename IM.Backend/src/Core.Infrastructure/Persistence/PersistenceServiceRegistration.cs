using Core.Infrastructure.Persistence.Contexts;
using Core.Infrastructure.Persistence.Repositories;
using Core.Infrastructure.Persistence.Repositories.Air;
using Core.Infrastructure.Persistence.Repositories.Land;
using Core.Infrastructure.Persistence.RepositoryContracts;
using Core.Infrastructure.Persistence.RepositoryContracts.Air;
using Core.Infrastructure.Persistence.RepositoryContracts.Land;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("RentACarConnectionString"))
        );

        services.AddScoped<IAirRepositoryManager, AirTransportRepositoryManager>();
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