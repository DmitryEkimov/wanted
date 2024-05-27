namespace Wanted.Services;

using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IBindEmployeeToCompanyService, BindEmployeeToCompanyService>();
        return services;
    }
}
