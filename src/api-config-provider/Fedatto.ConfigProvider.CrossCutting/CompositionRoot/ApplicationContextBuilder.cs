using Fedatto.ConfigProvider.Application;
using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Valor;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public class ApplicationContextBuilder : IContextBuilderInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAplicacaoApplication, AplicacaoApplication>();
        builder.Services.AddScoped<ITipoApplication, TipoApplication>();
        builder.Services.AddScoped<IChaveApplication, ChaveApplication>();
        builder.Services.AddScoped<IValorApplication, ValorApplication>();
    }
}
