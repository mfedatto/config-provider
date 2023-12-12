using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Wrappers;

namespace Fedatto.ConfigProvider.Domain.Valor;

public interface IValorApplication
{
    Task<IEnumerable<IValor<object>>> BuscarValores(
        CancellationToken cancellationToken,
        IChave chave,
        DateTime vigenteEm,
        bool habilitado = true);
    Task<IAplicacao?> BuscarAplicacaoPorId(
        CancellationToken cancellationToken,
        Guid appId);
    Task<IChave?> ObterChavePorId(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        int idChave);
}
