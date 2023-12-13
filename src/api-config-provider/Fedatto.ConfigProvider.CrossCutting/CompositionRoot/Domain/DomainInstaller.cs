using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.AppSettings;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Tipo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Domain;

public class DomainInstaller : IServiceInstaller, IServiceConfigurator
{
    public void Install(IServiceCollection services)
    {
        services.AddSingleton<AplicacaoFactory>();
        services.AddSingleton<TipoFactory>();
        services.AddSingleton<ChaveFactory>();
    }

    public void Configure(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigSection<DatabaseConfig>(configuration);
    }
}
