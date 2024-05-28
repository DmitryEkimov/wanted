namespace Wanted.Persistence.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<
            IReadRepository<Wanted.Domain.AggregateRoots.Company, Guid>,
            ReadCompanyRepository
        >();
        services.AddScoped<
            IReadRepository<Wanted.Domain.AggregateRoots.Employee, Guid>,
            ReadEmployeeRepository
        >();
        services.AddScoped<
            IWriteRepository<Wanted.Domain.AggregateRoots.Employee, Guid>,
            WriteEmployeeRepository
        >();
        services.AddScoped<
            IWriteRepository<Wanted.Domain.AggregateRoots.Employee, Guid>,
            WriteEmployeeRepository
        >();
        services.AddScoped<
            IWriteRelationsRepository<Domain.CompanyEmployeeRelationEntity>,
            WriteCompanyEmployeeRelationRepository
        >();
        return services;
    }
}
