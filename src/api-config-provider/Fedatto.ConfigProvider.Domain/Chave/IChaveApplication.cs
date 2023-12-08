using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Wrappers;

namespace Fedatto.ConfigProvider.Domain.Chave;

public interface IChaveApplication
{
    Task<PagedListWrapper<IChave>> BuscarChaves(
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
        Guid appId);
    Task<ITipo> BuscarTipoPorId(
        int id);
    Task<IChave> BuscarChavePorId(
        IAplicacao aplicacao,
        int id,
        DateTime vigenteEm);
    Task<IChave> IncluirChave(IChave chave);
}
