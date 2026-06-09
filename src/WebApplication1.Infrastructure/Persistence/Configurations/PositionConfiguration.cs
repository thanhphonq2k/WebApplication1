using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Infrastructure.Persistence.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<PositionEntity>
{
    public void Configure(EntityTypeBuilder<PositionEntity> builder)
    {
        builder.ToTable("Position", "dbo");
        builder.HasKey(p => p.PositionId);
        builder.Property(p => p.PositionId).HasColumnName("PositionId");
        builder.Property(p => p.PositionName).HasColumnName("PositionName").HasMaxLength(255);
        builder.Ignore(p => p.CreatedAt);
        builder.Ignore(p => p.UpdatedAt);
    }
}
