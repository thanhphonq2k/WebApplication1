using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.ToTable("Employee", "dbo");
        builder.HasKey(e => e.EmployeeId);
        builder.Property(e => e.EmployeeId).HasColumnName("EmployeeId");
        builder.Property(e => e.EmployeeName).HasColumnName("EmployeeName").HasMaxLength(255);
        builder.Property(e => e.Department).HasColumnName("Department").HasMaxLength(255);
        builder.Property(e => e.Position).HasColumnName("Position").HasMaxLength(255);
        builder.Property(e => e.DateOfJoining).HasColumnName("DateOfJoining");
        builder.Property(e => e.PhotoFileName).HasColumnName("PhotoFileName").HasMaxLength(255);
        builder.Ignore(e => e.CreatedAt);
        builder.Ignore(e => e.UpdatedAt);
    }
}
