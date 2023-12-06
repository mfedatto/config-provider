using Fedatto.ConfigProvider.CrossCutting.CompositionRoot.WebApi;
using Microsoft.AspNetCore.Builder;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public static class WebApplicationExtensions
{
    private static WebApplication Configure<T>(this WebApplication app)
        where T : IApplicationConfigurator, new()
    {
        new T().Configure(app);
        
        return app;
    }

    public static WebApplication ConfigureApp(this WebApplication app)
    {
        return app.Configure<WebApiConfigurator>();
    }
}
