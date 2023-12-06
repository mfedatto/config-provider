using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public interface IServiceInstaller
{
    void Install(IServiceCollection services);
}
