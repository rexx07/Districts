using Core.Domain.Entities.Air;
using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.EntityConfigurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.ToTable(nameof(Flight));

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();


        // // ref: https://learn.microsoft.com/en-us/ef/core/saving/concurrency?tabs=fluent-api
        builder.Property(r => r.Version).IsConcurrencyToken();

        builder
            .HasOne<Aircraft>()
            .WithMany()
            .HasForeignKey(p => p.AircraftId);

        builder
            .HasOne<Airport>()
            .WithMany()
            .HasForeignKey(d => d.DepartureAirportId)
            .HasForeignKey(a => a.ArriveAirportId);


        builder.Property(x => x.Status)
            .HasDefaultValue(FlightStatus.Unknown)
            .HasConversion(
                x => x.ToString(),
                x => (FlightStatus)Enum.Parse(typeof(FlightStatus), x));

        // // https://docs.microsoft.com/en-us/ef/core/modeling/shadow-properties
        // // https://docs.microsoft.com/en-us/ef/core/modeling/owned-entities
        // builder.OwnsMany(p => p.Seats, a =>
        // {
        //     a.WithOwner().HasForeignKey("FlightId");
        //     a.Property<long>("Id");
        //     a.HasKey("Id");
        //     a.Property<long>("FlightId");
        //     a.ToTable("Seat");
        // });
    }
}
