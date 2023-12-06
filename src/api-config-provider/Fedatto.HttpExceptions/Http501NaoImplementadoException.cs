namespace Fedatto.HttpExceptions;

public class Http501NaoImplementadoException : Http5xxServerException
{
    private const string HttpExceptionMessage = "HTTP 501 - Não implementado.";
    private const int HttpExceptionStatusCode = 501;

    public Http501NaoImplementadoException() : this(HttpExceptionMessage) { }

    public Http501NaoImplementadoException(string message) : base(message)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http501NaoImplementadoException(Exception innerException) : base(HttpExceptionMessage, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http501NaoImplementadoException(string message, Exception innerException) : base(message, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
}
