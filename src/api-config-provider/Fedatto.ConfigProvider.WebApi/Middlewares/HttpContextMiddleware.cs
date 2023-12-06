using Fedatto.ConfigProvider.Domain.MainDbContext;
using Fedatto.HttpExceptions;
using Newtonsoft.Json;

namespace Fedatto.ConfigProvider.WebApi.Middlewares;

public class HttpContextMiddleware
{
    private readonly RequestDelegate _next;

    public HttpContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUnitOfWork uow)
    {
        try
        {
            uow.BeginTransaction();

            await _next(context);

            uow.Commit();
        }
        catch (HttpException ex)
        {
            await HandleException(context, uow, ex);
        }
        catch (Exception ex)
        {
            await HandleException(context, uow, ex, 500);
        }
    }

    private async Task HandleException(HttpContext context, IUnitOfWork uow, HttpException httpException)
    {
        await HandleException(context, uow, httpException, httpException.StatusCode);
    }

    private async Task HandleException(HttpContext context, IUnitOfWork uow, Exception exception, int statusCode)
    {
        uow.Rollback();
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
        {
            Mensagem = exception.Message
        }));
    }
}
