using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;

namespace Fedatto.ConfigProvider.Domain.Valor;

public interface IValorService
{
    Task<IEnumerable<IValor<object>>> BuscarValores(
        CancellationToken cancellationToken,
        IChave chave,
        DateTime vigenteEm,
        bool habilitado = true);
    Task<IAplicacao?> BuscarAplicacaoPorId(
        CancellationToken cancellationToken,
        Guid appId);
    Task<IChave> BuscarChavePorId(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        int idChave);
}
