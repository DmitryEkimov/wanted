namespace Wanted.Persistence.Configurations;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class CompanyEmployeeConfig : IEntityTypeConfiguration<CompanyEmployee>
{
    public void Configure(EntityTypeBuilder<CompanyEmployee> builder)
    {
        builder.ToTable("CompanyEmployees").HasKey(x => new { x.CompanyId, x.EmployeeId });
        builder.Property(x => x.CompanyId).IsRequired();
        builder.Property(x => x.EmployeeId).IsRequired();
    }
}
