using System.Globalization;
using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Wanted.Bus;
using Wanted.Commands.AddEmployee;
using Wanted.Persistence;
using Wanted.Persistence.Extensions;
using Wanted.Services;
using Wanted.WebApi.Companies.Consumers;
using Wanted.WebApi.Companies.Features.Company;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatProvider: CultureInfo.GetCultureInfo("ru-RU"))
    .CreateLogger();
try
{
    Log.Information("starting server.");
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog(
        (context, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(context.Configuration)
    );
    builder.Services.Configure<RabbitMqConfig>(
        builder.Configuration.GetSection(nameof(RabbitMqConfig))
    );
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<DatabaseContext>(
        opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
        ServiceLifetime.Scoped,
        ServiceLifetime.Scoped
    );
    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(AddEmployeeHandler).Assembly)
    );
    builder.Services.AddMassTransit(x =>
    {
        x.AddConsumer<AddEmployeeConsumer>();
        x.UsingRabbitMq(
            (context, cfg) =>
            {
                var rabbitMqConfig = context.GetRequiredService<IOptions<RabbitMqConfig>>().Value;
                cfg.Host(
                    rabbitMqConfig.Host,
                    rabbitMqConfig.VirtualHost,
                    h =>
                    {
                        h.Username(rabbitMqConfig.Username);
                        h.Password(rabbitMqConfig.Password);
                    }
                );

                cfg.ConfigureEndpoints(context);
            }
        );
    });
    builder.Services.AddRepositories();
    builder.Services.AddBusinessServices();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    var app = builder.Build();
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        context.Database.Migrate();
    }
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.MapCompanyEmployeeEndpoints();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
