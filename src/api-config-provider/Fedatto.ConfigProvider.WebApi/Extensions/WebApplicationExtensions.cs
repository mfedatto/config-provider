using Fedatto.ConfigProvider.CrossCutting.CompositionRoot;
using Fedatto.ConfigProvider.WebApi.Middlewares;

namespace Fedatto.ConfigProvider.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication Configure(this WebApplication app)
    {
        return ((WebApplication)app.UseMiddleware<HttpContextMiddleware>())
            .ConfigureApp();
    }
}
