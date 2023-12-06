namespace Fedatto.HttpExceptions;

public class Http404NaoEncontradoException : Http4xxClientException
{
    private const string HttpExceptionMessage = "HTTP 404 - Não encontrado.";
    private const int HttpExceptionStatusCode = 404;

    public Http404NaoEncontradoException() : this(HttpExceptionMessage) { }
    
    public Http404NaoEncontradoException(string message) : base(message)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http404NaoEncontradoException(Exception innerException) : base(HttpExceptionMessage, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http404NaoEncontradoException(string message, Exception innerException) : base(message, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }

}
