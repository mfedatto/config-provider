using Microsoft.AspNetCore.Builder;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public interface IApplicationConfigurator
{
    IApplicationBuilder Configure(WebApplication app);
}
