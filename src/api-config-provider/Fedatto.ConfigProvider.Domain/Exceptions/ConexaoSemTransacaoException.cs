using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Domain.Exceptions;

public class ConexaoSemTransacaoException : Http500ErroInternoDoServidorException
{
    private const string HttpExceptionMessage = "Conexão sem transação.";

    public ConexaoSemTransacaoException() : base(HttpExceptionMessage) { }
    
    public ConexaoSemTransacaoException(Exception innerException) : base(HttpExceptionMessage, innerException) { }
}
