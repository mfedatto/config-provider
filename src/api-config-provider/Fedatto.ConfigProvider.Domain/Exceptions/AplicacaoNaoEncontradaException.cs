using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Domain.Exceptions;

public class AplicacaoNaoEncontradaException : Http404NaoEncontradoException
{
    private const string HttpExceptionMessage = "Aplicação não encontrada.";

    public AplicacaoNaoEncontradaException() : base(HttpExceptionMessage) { }
    
    public AplicacaoNaoEncontradaException(Exception innerException) : base(HttpExceptionMessage, innerException) { }
}
