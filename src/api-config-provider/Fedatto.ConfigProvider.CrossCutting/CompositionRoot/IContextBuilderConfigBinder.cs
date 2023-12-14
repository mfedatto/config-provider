using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public interface IContextBuilderConfigBinder
{
    void BindConfig(
        WebApplicationBuilder builder,
        IConfiguration configuration);
}
