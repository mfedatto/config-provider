using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Domain.Exceptions;

public class SiglaDeAplicacaoEmUsoException : Http409ConflitoException
{
    private const string HttpExceptionMessage = "Sigla já em uso por uma aplicação.";
    
    public SiglaDeAplicacaoEmUsoException() : base(HttpExceptionMessage) { }
    
    public SiglaDeAplicacaoEmUsoException(Exception innerException) : base(HttpExceptionMessage, innerException) { }
}
