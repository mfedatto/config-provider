namespace Fedatto.HttpExceptions;

public class Http500ErroInternoDoServidorException : Http5xxServerException
{
    private const string HttpExceptionMessage = "HTTP 500 - Erro interno do servidor.";
    private const int HttpExceptionStatusCode = 500;

    public Http500ErroInternoDoServidorException() : this(HttpExceptionMessage) { }

    public Http500ErroInternoDoServidorException(string message) : base(message)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http500ErroInternoDoServidorException(Exception innerException) : base(HttpExceptionMessage, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http500ErroInternoDoServidorException(string message, Exception innerException) : base(message, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
}
