using Core.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.EntityConfigurations;

public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
{
    public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
    {
        builder.ToTable("UserOperationClaims").HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("Id");
        builder.Property(u => u.UserId).HasColumnName("UserId");
        builder.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
        builder
            .HasIndex(indexExpression: u => new { u.UserId, u.OperationClaimId },
                      name: "UK_UserOperationClaims_UserId_OperationClaimId")
            .IsUnique();
        builder.HasOne(u => u.User);
        builder.HasOne(u => u.OperationClaim);
    }
}