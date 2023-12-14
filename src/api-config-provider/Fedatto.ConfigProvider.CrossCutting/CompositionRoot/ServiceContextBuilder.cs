using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Valor;
using Fedatto.ConfigProvider.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public class ServiceContextBuilder : IContextBuilderInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAplicacaoService, AplicacaoService>();
        builder.Services.AddScoped<ITipoService, TipoService>();
        builder.Services.AddScoped<IChaveService, ChaveService>();
        builder.Services.AddScoped<IValorService, ValorService>();
    }
}
