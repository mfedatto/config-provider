namespace Fedatto.HttpExceptions;

public class Http400RequisicaoInvalidaException : Http4xxClientException
{
    private const string HttpExceptionMessage = "HTTP 400 - Requisição inválida.";
    private const int HttpExceptionStatusCode = 400;

    public Http400RequisicaoInvalidaException() : this(HttpExceptionMessage) { }

    public Http400RequisicaoInvalidaException(string message) : base(message)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http400RequisicaoInvalidaException(Exception innerException) : base(HttpExceptionMessage, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http400RequisicaoInvalidaException(string message, Exception innerException) : base(message, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
}
