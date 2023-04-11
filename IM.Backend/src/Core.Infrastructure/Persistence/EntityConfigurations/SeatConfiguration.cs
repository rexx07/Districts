using Core.Domain.Entities.Air;
using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.EntityConfigurations;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable(nameof(Seat));

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();

        // // ref: https://learn.microsoft.com/en-us/ef/core/saving/concurrency?tabs=fluent-api
         builder.Property(r => r.Version).IsConcurrencyToken();

        builder
            .HasOne<Flight>()
            .WithMany()
            .HasForeignKey(p => p.FlightId);

        builder.Property(x => x.Class)
            .HasDefaultValue(SeatClass.Unknown)
            .HasConversion(
                x => x.ToString(),
                x => (SeatClass)Enum.Parse(typeof(SeatClass), x));

        builder.Property(x => x.Type)
            .HasDefaultValue(SeatType.Unknown)
            .HasConversion(
                x => x.ToString(),
                x => (SeatType)Enum.Parse(typeof(SeatType), x));
    }
}
