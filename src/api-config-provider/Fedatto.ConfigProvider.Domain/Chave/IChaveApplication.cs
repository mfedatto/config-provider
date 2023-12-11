using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Wrappers;

namespace Fedatto.ConfigProvider.Domain.Chave;

public interface IChaveApplication
{
    Task<PagedListWrapper<IChave>> BuscarChaves(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        DateTime vigenteEm,
        string? nome = null,
        ITipo? tipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true,
        int? skip = 0,
        int? limit = null);
    Task<IAplicacao> BuscarAplicacaoPorId(
        CancellationToken cancellationToken,
        Guid appId);
    Task<ITipo> BuscarTipoPorId(
        CancellationToken cancellationToken,
        int id);
    Task<IChave> BuscarChavePorId(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        int id);
    Task<IChave> IncluirChave(
        CancellationToken cancellationToken,
        IChave chave);
    Task<IChave> AlterarChave(
        CancellationToken cancellationToken,
        IChave chaveAAlterar);
    Task ExcluirChave(
        CancellationToken cancellationToken,
        int id);
}
