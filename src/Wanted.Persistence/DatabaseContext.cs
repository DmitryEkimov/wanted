namespace Wanted.Persistence;

using Entities;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;

public sealed class DatabaseContext(DbContextOptions options) : DbContext(options), IAppContext
{
    internal DbSet<Company> Companies { get; set; }
    internal DbSet<Employee> Employees { get; set; }

    internal DbSet<CompanyEmployee> CompanyEmployees { get; set; }

    public Task<bool> CanConnectAsync() => this.Database.CanConnectAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        modelBuilder
            .Entity<Company>()
            .HasMany(e => e.Employees)
            .WithMany(e => e.Companies)
            .UsingEntity<CompanyEmployee>(
                l => l.HasOne<Employee>().WithMany().HasForeignKey(e => e.EmployeeId),
                r => r.HasOne<Company>().WithMany().HasForeignKey(e => e.CompanyId)
            );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseExceptionProcessor();
}
