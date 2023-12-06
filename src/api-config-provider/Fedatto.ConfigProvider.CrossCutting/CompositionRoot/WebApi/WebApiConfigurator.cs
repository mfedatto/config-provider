using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot.WebApi;

public class WebApiConfigurator : IApplicationConfigurator
{
    public IApplicationBuilder Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapPrometheusScrapingEndpoint();
        
        //app.UseHttpsRedirection();
        
        app.UseRouting();
        app.UseMvc();

        return app;
    }
}
