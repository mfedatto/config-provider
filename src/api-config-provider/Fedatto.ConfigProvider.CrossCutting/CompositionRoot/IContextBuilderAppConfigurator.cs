using Microsoft.AspNetCore.Builder;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public interface IContextBuilderAppConfigurator
{
    WebApplication Configure(
        WebApplication app);
}
