using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.Domain.Chave;

public interface IChaveService
{
    Task<IEnumerable<IChave>> BuscarChaves(
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
    Task<int> ContarChaves(
        IAplicacao aplicacao,
        DateTime vigenteEm,
        string? nome = null,
        ITipo? tipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true);
    Task<IChave> BuscarChavePorId(
        IAplicacao aplicacao,
        int id,
        DateTime vigenteEm);
    Task<IChave> IncluirChave(
        IChave chave);
}
