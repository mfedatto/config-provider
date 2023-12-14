using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Domain.Exceptions;

public class ConexaoEmUsoPorOutraTransacaoException : Http500ErroInternoDoServidorException
{
    private const string HttpExceptionMessage = "Conexão em uso por outra transação.";

    public ConexaoEmUsoPorOutraTransacaoException() : base(HttpExceptionMessage) { }
    
    public ConexaoEmUsoPorOutraTransacaoException(Exception innerException) : base(HttpExceptionMessage, innerException) { }
}
