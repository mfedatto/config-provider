using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Domain.Exceptions;

public class MaisDeUmaAplicacaoEncontradaException : Http409ConflitoException
{
    private const string HttpExceptionMessage = "Mais de uma aplicação encontrada.";

    public MaisDeUmaAplicacaoEncontradaException() : base(HttpExceptionMessage) { }
    
    public MaisDeUmaAplicacaoEncontradaException(Exception innerException) : base(HttpExceptionMessage, innerException) { }
}
