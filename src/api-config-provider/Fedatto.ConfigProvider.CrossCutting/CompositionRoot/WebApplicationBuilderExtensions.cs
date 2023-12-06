using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Application;
using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Domain;
using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Infrastructure;
using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Service;
using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public static class WebApplicationBuilderExtensions
{
    private static IServiceCollection InstallServices<T>(this IServiceCollection services)
        where T : IServiceInstaller, new()
    {
        new T().Install(services);
        
        return services;
    }

    public static WebApplicationBuilder AddCompositionRoot(this WebApplicationBuilder builder)
    {
        builder.Services
            .InstallServices<InfrastructureInstaller>()
            .InstallServices<ServiceInstaller>()
            .InstallServices<ApplicationInstaller>()
            .InstallServices<DomainInstaller>()
            .InstallServices<WebApiInstaller>();
        
        return builder;
    }
}
