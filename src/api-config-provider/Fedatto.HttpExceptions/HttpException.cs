namespace Fedatto.HttpExceptions;

public class HttpException : Exception
{
    private const string HttpExceptionMessage = "HTTP Exception.";

    public int StatusCode { get; protected init; }

    protected HttpException() : base(HttpExceptionMessage) { }

    protected HttpException(string message) : base(message) { }

    protected HttpException(Exception innerException) : base(HttpExceptionMessage, innerException) { }
    
    protected HttpException(string message, Exception innerException) : base(message, innerException) { }
}
