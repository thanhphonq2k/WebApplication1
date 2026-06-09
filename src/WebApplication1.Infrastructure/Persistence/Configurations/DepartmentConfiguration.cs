using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Infrastructure.Persistence.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<DepartmentEntity>
{
    public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
    {
        builder.ToTable("Department", "dbo");
        builder.HasKey(d => d.DepartmentId);
        builder.Property(d => d.DepartmentId).HasColumnName("DepartmentId");
        builder.Property(d => d.DepartmentName).HasColumnName("DepartmentName").HasMaxLength(255);
        builder.Ignore(d => d.CreatedAt);
        builder.Ignore(d => d.UpdatedAt);
    }
}
