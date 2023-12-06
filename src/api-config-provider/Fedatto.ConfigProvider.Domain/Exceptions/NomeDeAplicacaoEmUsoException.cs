using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Domain.Exceptions;

public class NomeDeAplicacaoEmUsoException : Http409ConflitoException
{
    private const string HttpExceptionMessage = "Nome já em uso por uma aplicação.";
    
    public NomeDeAplicacaoEmUsoException() : base(HttpExceptionMessage) { }
    
    public NomeDeAplicacaoEmUsoException(Exception innerException) : base(HttpExceptionMessage, innerException) { }
}
