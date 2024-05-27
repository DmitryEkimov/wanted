using System.Globalization;
using System.Reflection;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Options;
using Serilog;
using Wanted.Bus;
using Wanted.Bus.Contracts;
using Wanted.WebApi.Employees.Features.Employee;

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
    builder.Services.AddMassTransit(x =>
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
        )
    );
    builder.Services.AddScoped<IValidator<AddEmployeeContract>, AddEmployeeContractValidation>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.MapEmployeeEndpoints();
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
