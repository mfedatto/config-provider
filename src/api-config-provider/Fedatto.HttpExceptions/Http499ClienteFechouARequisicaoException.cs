namespace Fedatto.HttpExceptions;

public class Http499ClienteFechouARequisicao : Http4xxClientException
{
    private const string HttpExceptionMessage = "HTTP 499 - Cliente fechou a requisição.";
    private const int HttpExceptionStatusCode = 499;

    public Http499ClienteFechouARequisicao() : this(HttpExceptionMessage) { }
    
    public Http499ClienteFechouARequisicao(string message) : base(message)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http499ClienteFechouARequisicao(Exception innerException) : base(HttpExceptionMessage, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }
    
    public Http499ClienteFechouARequisicao(string message, Exception innerException) : base(message, innerException)
    {
        StatusCode = HttpExceptionStatusCode;
    }

}
