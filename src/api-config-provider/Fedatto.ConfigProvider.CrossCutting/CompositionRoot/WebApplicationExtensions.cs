using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Application;
using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Domain;
using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Infrastructure;
using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Service;
using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.WebApi;
using Fedatto.ConfigProvider.Domain.AppSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public static class WebApplicationExtensions
{
    #region Service Configurator Extensions

    public static IServiceCollection ConfigSection<T>(
        this IServiceCollection services,
        IConfiguration configuration)
    where T : class, IConfig, new()
    {
        T configurator = new();

        configuration.GetSection(configurator.Section)
            .Bind(configurator);
        
        return services.AddSingleton(configurator);
    }

    #endregion

    #region Service Installer Extensions

    public static WebApplicationBuilder AddCompositionRoot(
        this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.CreateConfiguration();

        builder.Services
            .InstallServices<InfrastructureInstaller>(configuration)
            .InstallServices<ServiceInstaller>(configuration)
            .InstallServices<ApplicationInstaller>(configuration)
            .InstallServices<DomainInstaller>(configuration)
            .InstallServices<WebApiInstaller>(configuration);

        return builder;
    }

    public static IConfiguration CreateConfiguration(
        this WebApplicationBuilder builder)
    {
        return builder.Configuration
            .AddJsonFile(
                "appsettings.json",
                false,
                true)
            .AddJsonFile(
                $"appsettings{builder.Environment.EnvironmentName}.json",
                true,
                true)
            .AddEnvironmentVariables()
            .Build();
    }

    private static IServiceCollection InstallServices<T>(
        this IServiceCollection services,
        IConfiguration configuration)
        where T : IServiceInstaller, new()
    {
        T installer = new();

        if (installer is IServiceConfigurator configurator)
        {
            configurator.Configure(services, configuration);
        }

        installer.Install(services);

        return services;
    }

    #endregion

    #region Application Configurator Extensions

    private static WebApplication Configure<T>(this WebApplication app)
        where T : IApplicationConfigurator, new()
    {
        new T().Configure(app);

        return app;
    }

    public static WebApplication ConfigureApp(this WebApplication app)
    {
        return app.Configure<WebApiConfigurator>();
    }

    #endregion
}
