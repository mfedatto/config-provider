using Fedatto.ConfigProvider.Domain.Chave;

namespace Fedatto.ConfigProvider.Domain.Valor;

public interface IValorService
{
    Task<IEnumerable<IValor<object>>> BuscarValores(
        IChave chave,
        DateTime vigenteEm,
        bool habilitado = true);
    Task<bool> AplicacaoExiste(
        Guid appId);
    Task<IChave> BuscarChavePorId(
        Guid appId,
        int idChave,
        DateTime vigenteEm);
}
