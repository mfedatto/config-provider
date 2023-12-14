using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public class WebApiContextBuilder : IContextBuilderInstaller, IContextBuilderAppConfigurator
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
    
    public WebApplication Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();
        
        app.UseRouting();
        app.UseMvc();

        return app;
    }
}
