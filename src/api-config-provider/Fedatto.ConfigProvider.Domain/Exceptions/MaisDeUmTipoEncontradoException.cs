using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Domain.Exceptions;

public class MaisDeUmTipoEncontradoException : Http409ConflitoException
{
    private const string HttpExceptionMessage = "Mais de uma aplicação encontrada.";

    public MaisDeUmTipoEncontradoException() : base(HttpExceptionMessage) { }
    
    public MaisDeUmTipoEncontradoException(Exception innerException) : base(HttpExceptionMessage, innerException) { }
}
