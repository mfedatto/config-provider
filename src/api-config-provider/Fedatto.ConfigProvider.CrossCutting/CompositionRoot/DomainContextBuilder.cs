using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Extensions;
using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.AppSettings;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Tipo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public class DomainContextBuilder : IContextBuilderInstaller, IContextBuilderConfigBinder
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<AplicacaoFactory>();
        builder.Services.AddSingleton<TipoFactory>();
        builder.Services.AddSingleton<ChaveFactory>();
    }

    public void BindConfig(
        WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.BindConfig<DatabaseConfig>(configuration);
        builder.BindConfig<TelemetryConfig>(configuration);
    }
}
