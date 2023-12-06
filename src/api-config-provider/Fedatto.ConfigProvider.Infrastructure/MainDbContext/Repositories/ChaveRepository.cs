using Fedatto.HttpExceptions;
using Dapper;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.MainDbContext;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext.Repositories;

public class ChaveRepository : IChaveRepository
{
    private readonly IUnitOfWork _uow;

    public ChaveRepository(
        IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<IEnumerable<IChave>> BuscarChaves(
        Guid appId,
        DateTime vigenteEm,
        string? nome = null,
        int? idTipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true,
        int? skip = 0,
        int? limit = null)
    {
        return await _uow.DbConnection.QueryAsync<Chave>(
            """
            SELECT *
            FROM Chaves
            WHERE
                (AppId = @p_AppId::uuid) AND
                (@p_Nome IS NULL OR LOWER(Nome) ~ @p_Nome) AND
                (@p_IdTipo IS NULL OR IdTipo = @p_IdTipo) AND
                (@p_Lista IS NULL OR Lista = @p_Lista) AND
                (@p_PermiteNulo IS NULL OR PermiteNulo = @p_PermiteNulo) AND
                (@p_IdTipo IS NULL OR IdTipo = @p_IdTipo) AND
                (Habilitado = @p_Habilitado) AND
                (VigenteDe IS NULL OR VigenteDe <= @p_VigenteEm::date) AND
                (VigenteAte IS NULL OR VigenteAte >= @p_VigenteEm::date)
            ORDER BY Nome
            OFFSET @p_Skip
            LIMIT @p_Limit;
            """,
            new
            {
                p_AppId = appId,
                p_VigenteEm = vigenteEm,
                p_Nome = nome?.ToLower(),
                p_IdTipo = idTipo,
                p_Lista = lista,
                p_PermiteNulo = permiteNulo,
                p_IdChavePai = idChavePai,
                p_Habilitado = habilitado,
                p_Skip = skip,
                p_Limit = limit
            });
    }

    public async Task<int> ContarChaves(
        Guid appId,
        DateTime vigenteEm,
        string? nome = null,
        int? idTipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true)
    {
        return await _uow.DbConnection.ExecuteScalarAsync<int>(
            """
            SELECT COUNT(*)
            FROM Chaves
            WHERE
                (AppId = @p_AppId::uuid) AND
                (@p_Nome IS NULL OR LOWER(Nome) ~ @p_Nome) AND
                (@p_IdTipo IS NULL OR IdTipo = @p_IdTipo) AND
                (@p_Lista IS NULL OR Lista = @p_Lista) AND
                (@p_PermiteNulo IS NULL OR PermiteNulo = @p_PermiteNulo) AND
                (@p_IdTipo IS NULL OR IdTipo = @p_IdTipo) AND
                (Habilitado = @p_Habilitado) AND
                (VigenteDe IS NULL OR VigenteDe <= @p_VigenteEm::date) AND
                (VigenteAte IS NULL OR VigenteAte >= @p_VigenteEm::date)
            """,
            new
            {
                p_AppId = appId,
                p_VigenteEm = vigenteEm,
                p_Nome = nome?.ToLower(),
                p_IdTipo = idTipo,
                p_Lista = lista,
                p_PermiteNulo = permiteNulo,
                p_IdChavePai = idChavePai,
                p_Habilitado = habilitado
            });
    }

    public async Task<IChave> BuscarChavePorId(
        Guid appId,
        int id,
        DateTime vigenteEm)
    {
        return (await _uow.DbConnection.QueryAsync<Chave>(
                """
                SELECT *
                FROM Chaves
                WHERE
                    AppId = @p_AppId::uuid AND
                    Id = @p_Id AND
                    (VigenteDe IS NULL OR VigenteDe <= @p_VigenteEm::date) AND
                    (VigenteAte IS NULL OR VigenteAte >= @p_VigenteEm::date)
                """,
                new
                {
                    p_AppId = appId,
                    p_Id = id,
                    p_VigenteEm = vigenteEm
                }))
            .SingleOrDefault<IChave>()!;
    }
}

file record Chave : IChave
{
    public int Id { get; init; }
    public Guid AppId { get; init; }
    public required string Nome { get; init; }
    public int IdTipo { get; init; }
    public bool Lista { get; init; }
    public bool PermiteNulo { get; init; }
    public int? IdChavePai { get; init; }
    public bool Habilitado { get; init; }
    public DateTime? VigenteDe { get; init; }
    public DateTime? VigenteAte { get; init; }
}
