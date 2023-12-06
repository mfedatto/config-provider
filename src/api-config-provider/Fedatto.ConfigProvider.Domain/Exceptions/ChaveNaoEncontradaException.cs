using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Domain.Exceptions;

public class ChaveNaoEncontradaException : Http404NaoEncontradoException
{
    private const string HttpExceptionMessage = "Chave não encontrada.";

    public ChaveNaoEncontradaException() : base(HttpExceptionMessage) { }
    
    public ChaveNaoEncontradaException(Exception innerException) : base(HttpExceptionMessage, innerException) { }
}
