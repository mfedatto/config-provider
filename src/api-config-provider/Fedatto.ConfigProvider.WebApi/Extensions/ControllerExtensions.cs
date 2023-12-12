using Microsoft.AspNetCore.Mvc;

namespace Fedatto.ConfigProvider.WebApi.Extensions;

public static class ControllerExtensions
{
    public static OkObjectResult HttpOk(this object? value)
    {
        return new OkObjectResult(value);
    }
}
