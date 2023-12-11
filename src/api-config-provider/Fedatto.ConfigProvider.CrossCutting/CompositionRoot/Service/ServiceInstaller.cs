using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Valor;
using Fedatto.ConfigProvider.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Service;

public class ServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<IAplicacaoService, AplicacaoService>();
        services.AddScoped<ITipoService, TipoService>();
        services.AddScoped<IChaveService, ChaveService>();
        services.AddScoped<IValorService, ValorService>();
    }
}
