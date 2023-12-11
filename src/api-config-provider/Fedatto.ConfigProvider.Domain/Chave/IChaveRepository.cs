using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.Domain.Chave;

public interface IChaveRepository
{
    Task<IEnumerable<IChave>> BuscarChaves(
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
    Task<int> ContarChaves(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        DateTime vigenteEm,
        string? nome = null,
        ITipo? tipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true);
    Task<IChave> BuscarChavePorId(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        int id);
    Task<IChave> IncluirChave(
        CancellationToken cancellationToken,
        IChave chaveAIncluir);
    Task<IChave> AlterarChave(
        CancellationToken cancellationToken,
        IChave chaveAAlterar);
    Task ExcluirChave(
        CancellationToken cancellationToken,
        int idChave);
}
