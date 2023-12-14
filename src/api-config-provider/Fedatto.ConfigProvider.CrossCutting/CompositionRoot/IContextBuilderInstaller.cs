using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public interface IContextBuilderInstaller
{
    void Install(
        WebApplicationBuilder builder);
}
