using Fedatto.ConfigProvider.Application;
using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Tipo;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Application;

public class ApplicationInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<IAplicacaoApplication, AplicacaoApplication>();
        services.AddScoped<ITipoApplication, TipoApplication>();
        services.AddScoped<IChaveApplication, ChaveApplication>();
    }
}
