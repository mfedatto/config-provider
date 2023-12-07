using Fedatto.ConfigProvider.Domain.Wrappers;

namespace Fedatto.ConfigProvider.Domain.Valor;

public interface IValorApplication
{
    Task<IEnumerable<IValor<object>>> BuscarValores(
        Guid appId,
        int idChave,
        DateTime vigenteEm,
        bool habilitado = true);
    Task<bool> AplicacaoExiste(
        Guid appId);
    Task<bool> ChaveExiste(
        Guid appId,
        int idChave,
        DateTime vigenteEm);
}
