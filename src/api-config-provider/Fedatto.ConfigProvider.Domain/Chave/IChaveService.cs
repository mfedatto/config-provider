namespace Fedatto.ConfigProvider.Domain.Chave;

public interface IChaveService
{
    Task<IEnumerable<IChave>> BuscarChaves(
        Guid appId,
        DateTime vigenteEm,
        string? nome = null,
        int? idTipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true,
        int? skip = 0,
        int? limit = null);
    Task<int> ContarChaves(
        Guid appId,
        DateTime vigenteEm,
        string? nome = null,
        int? idTipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true);
    Task<IChave> BuscarChavePorId(
        Guid appId,
        int id,
        DateTime vigenteEm);
    Task<IChave> IncluirChave(
        IChave chave);
}
