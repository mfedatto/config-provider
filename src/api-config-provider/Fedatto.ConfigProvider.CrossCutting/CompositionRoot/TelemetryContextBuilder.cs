using Fedatto.ConfigProvider.Domain.AppSettings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Fedatto.ConfigProvider.CrossCutting.CompositionRoot;

public class TelemetryContextBuilder : IContextBuilderInstaller, IContextBuilderAppConfigurator
{
    public void Install(WebApplicationBuilder builder)
    {
        TelemetryConfig config = builder.Services.BuildServiceProvider()
            .GetService<TelemetryConfig>()!;
        string? tracingOtlpEndpoint = config.OtlpEndpointUrl;
        OpenTelemetryBuilder otel = builder.Services.AddOpenTelemetry();

        // Configure OpenTelemetry Resources with the application name
        otel.ConfigureResource(resource => resource
            .AddService(builder.Environment.ApplicationName));

        // Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
        otel.WithMetrics(metrics =>
        {
            metrics.AddAspNetCoreInstrumentation();

            foreach (string meter in config.Meters!)
            {
                metrics.AddMeter(meter);
            }
            
            metrics.AddPrometheusExporter();
        });

        // Add Tracing for ASP.NET Core and our custom ActivitySource and export to Jaeger
        otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation();
            tracing.AddHttpClientInstrumentation();

            if (tracingOtlpEndpoint is not null)
            {
                tracing.AddOtlpExporter(otlpOptions => { otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint); });
            }
            else
            {
                tracing.AddConsoleExporter();
            }
        });
    }

    public WebApplication Configure(WebApplication app)
    {
        // Configure the Prometheus scraping endpoint
        app.MapPrometheusScrapingEndpoint();

        return app;
    }
}
