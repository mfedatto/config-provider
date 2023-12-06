using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.MainDbContext;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Infrastructure.MainDbContext;
using Fedatto.ConfigProvider.Infrastructure.MainDbContext.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot.Infrastructure;

public class InfrastructureInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAplicacaoRepository, AplicacaoRepository>();
        services.AddScoped<ITipoRepository, TipoRepository>();
        services.AddScoped<IChaveRepository, ChaveRepository>();
    }
}
