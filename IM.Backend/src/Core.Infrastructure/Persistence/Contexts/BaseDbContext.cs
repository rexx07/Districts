using System.Reflection;
using Core.Domain.Entities;
using Core.Domain.Entities.Air;
using Core.Domain.Entities.Land;
using Core.Domain.Entities.PassengerAndCargo;
using Core.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace Core.Infrastructure.Persistence.Contexts;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected IConfiguration Configuration { get; set; }
    public DbSet<AdditionalService> AdditionalServices { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Vehicle> Cars { get; set; }
    public DbSet<VehicleDamage> CarDamages { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<CorporateCustomer> CorporateCustomers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<FindeksCreditRate> FindeksCreditRates { get; set; }
    public DbSet<Fuel> Fuel { get; set; }
    public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<RentalsAdditionalService> RentalsAdditionalServices { get; set; }
    public DbSet<RentalBranch> RentalBranches { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Transmission> Transmissions { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<Aircraft> Aircrafts { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Passenger> Passengers { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        IEnumerable<EntityEntry<Entity>> entries = ChangeTracker
                                                   .Entries<Entity>()
                                                   .Where(e => e.State == EntityState.Added ||
                                                               e.State == EntityState.Modified);

        foreach (EntityEntry<Entity> entry in entries)
            _ = entry.State switch
                {
                    EntityState.Added => entry.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => entry.Entity.UpdatedDate = DateTime.UtcNow
                };
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}