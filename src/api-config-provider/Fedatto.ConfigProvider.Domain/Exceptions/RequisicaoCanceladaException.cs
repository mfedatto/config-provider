using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Domain.Exceptions;

public class RequisicaoCanceladaException : Http499ClienteFechouARequisicao
{
    private const string HttpExceptionMessage = "Requisição cancelada.";
    
    public RequisicaoCanceladaException() : base(HttpExceptionMessage) { }
    
    public RequisicaoCanceladaException(Exception innerException) : base(HttpExceptionMessage, innerException) { }
}
