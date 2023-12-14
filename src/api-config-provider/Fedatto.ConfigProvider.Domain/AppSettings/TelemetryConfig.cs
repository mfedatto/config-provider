namespace Fedatto.ConfigProvider.Domain.AppSettings;

public class TelemetryConfig : IConfig
{
    public string Section => "Telemetry";
    
    public string? OtlpEndpointUrl { get; set; }
    public string[]? Meters { get; set; }
}
