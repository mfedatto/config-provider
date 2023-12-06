using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Tipo;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Domain;

public class DomainInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddSingleton<AplicacaoFactory>();
        services.AddSingleton<TipoFactory>();
    }
}
