using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Domain.Exceptions;

public class MaisDeUmaChaveEncontradaException : Http409ConflitoException
{
    private const string HttpExceptionMessage = "Mais de uma aplicação encontrada.";

    public MaisDeUmaChaveEncontradaException() : base(HttpExceptionMessage) { }
    
    public MaisDeUmaChaveEncontradaException(Exception innerException) : base(HttpExceptionMessage, innerException) { }
}
