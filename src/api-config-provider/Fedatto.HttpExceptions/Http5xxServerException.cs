namespace Fedatto.HttpExceptions;

public class Http5xxServerException : HttpException
{
    private const string HttpExceptionMessage = "HTTP 500 - Server error.";
    private const int HttpExceptionStatusCode = 500;

    public Http5xxServerException() : this(HttpExceptionMessage) { }

    public Http5xxServerException(string message) : base(message)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    protected Http5xxServerException(Exception innerException) : base(HttpExceptionMessage, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http5xxServerException(string message, Exception innerException) : base(message, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
}
