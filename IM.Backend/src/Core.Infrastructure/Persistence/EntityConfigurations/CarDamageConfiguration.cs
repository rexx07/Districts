﻿using Core.Domain.Entities.Land;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.EntityConfigurations;

public class CarDamageConfiguration : IEntityTypeConfiguration<VehicleDamage>
{
    public void Configure(EntityTypeBuilder<VehicleDamage> builder)
    {
        builder.ToTable("CarDamages").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.CarId).HasColumnName("CarId");
        builder.Property(p => p.IsFixed).HasColumnName("IsFixed").HasDefaultValue(false);
        builder.HasOne(p => p.Car);
    }
}