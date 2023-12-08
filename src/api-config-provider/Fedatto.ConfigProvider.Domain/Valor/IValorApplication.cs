using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Wrappers;

namespace Fedatto.ConfigProvider.Domain.Valor;

public interface IValorApplication
{
    Task<IEnumerable<IValor<object>>> BuscarValores(
        IChave chave,
        DateTime vigenteEm,
        bool habilitado = true);
    Task<IAplicacao?> BuscarAplicacaoPorId(
        Guid appId);
    Task<IChave?> ObterChavePorId(
        IAplicacao aplicacao,
        int idChave,
        DateTime vigenteEm);
}
