namespace Fedatto.HttpExceptions;

public class Http409ConflitoException : Http4xxClientException
{
    private const string HttpExceptionMessage = "HTTP 409 - Conflito.";
    private const int HttpExceptionStatusCode = 409;

    public Http409ConflitoException() : this(HttpExceptionMessage) { }
    
    public Http409ConflitoException(string message) : base(message)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http409ConflitoException(Exception innerException) : base(HttpExceptionMessage, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http409ConflitoException(string message, Exception innerException) : base(message, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }

}
