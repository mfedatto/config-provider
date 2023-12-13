using Fedatto.ConfigProvider.Domain.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot.WebApi;

public class WebApiInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddMvc(options => options.EnableEndpointRouting = false);
        services.AddOpenTelemetry()
            .WithMetrics(builder =>
            {
                builder.AddPrometheusExporter();
                builder.AddMeter(
                    "Microsoft.AspNetCore.Hosting",
                    "Microsoft.AspNetCore.Kestrel",
                    "System.Net.Http"
                );
            });
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
