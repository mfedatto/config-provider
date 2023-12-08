using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;

namespace Fedatto.ConfigProvider.Domain.Valor;

public interface IValorService
{
    Task<IEnumerable<IValor<object>>> BuscarValores(
        IChave chave,
        DateTime vigenteEm,
        bool habilitado = true);
    Task<IAplicacao?> BuscarAplicacaoPorId(
        Guid appId);
    Task<IChave> BuscarChavePorId(
        IAplicacao aplicacao,
        int idChave,
        DateTime vigenteEm);
}
