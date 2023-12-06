namespace Fedatto.HttpExceptions;

// ReSharper disable once InconsistentNaming
public class Http4xxClientException : HttpException
{
    private const string HttpExceptionMessage = "HTTP 400 - Bad Request.";
    private const int HttpExceptionStatusCode = 400;

    public Http4xxClientException() : this(HttpExceptionMessage) { }

    public Http4xxClientException(string message) : base(message)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http4xxClientException(Exception innerException) : base(HttpExceptionMessage, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http4xxClientException(string message, Exception innerException) : base(message, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
}
