using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public interface IServiceConfigurator
{
    void Configure(IServiceCollection services, IConfiguration configuration);
}
