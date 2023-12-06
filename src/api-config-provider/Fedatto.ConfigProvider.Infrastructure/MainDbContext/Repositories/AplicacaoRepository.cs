using Dapper;
using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.MainDbContext;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext.Repositories;

public class AplicacaoRepository : IAplicacaoRepository
{
    private readonly IUnitOfWork _uow;

    public AplicacaoRepository(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<IAplicacao>> BuscarAplicacoes(
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null,
        int? skip = 0,
        int? limit = null)
    {
        return await _uow.DbConnection.QueryAsync<Aplicacao>(
            """
            SELECT *
            FROM Aplicacoes
            WHERE
                (@p_Nome IS NULL OR LOWER(Nome) ~ @p_Nome) AND
                (@p_Sigla IS NULL OR LOWER(Sigla) ~ @p_Sigla) AND
                (@p_Aka IS NULL OR LOWER(Aka) ~ @p_Aka) AND
                (@p_Habilitado IS NULL OR Habilitado = @p_Habilitado) AND
                (@p_VigenteEm IS NULL OR (VigenteDe IS NULL OR VigenteDe <= @p_VigenteEm::date) AND (VigenteAte IS NULL OR VigenteAte >= @p_VigenteEm::date))
            ORDER BY Nome
            OFFSET @p_Skip
            LIMIT @p_Limit;
            """,
            new
            {
                p_Nome = nome?.ToLower(),
                p_Sigla = sigla?.ToLower(),
                p_Aka = aka?.ToLower(),
                p_Habilitado = habilitado,
                p_VigenteEm = vigenteEm?.ToString("yyyy-MM-dd HH:mm:ss"),
                p_Skip = skip,
                p_Limit = limit
            });
    }

    public async Task<int> ContarAplicacoes(
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null)
    {
        return await _uow.DbConnection.ExecuteScalarAsync<int>(
            """
            SELECT COUNT(*)
            FROM Aplicacoes
            WHERE
                (@p_Nome IS NULL OR LOWER(Nome) ~ @p_Nome) AND
                (@p_Sigla IS NULL OR LOWER(Sigla) ~ @p_Sigla) AND
                (@p_Aka IS NULL OR LOWER(Aka) ~ @p_Aka) AND
                (@p_Habilitado IS NULL OR Habilitado = @p_Habilitado) AND
                (@p_VigenteEm IS NULL OR (VigenteDe IS NULL OR VigenteDe <= @p_VigenteEm::date) AND (VigenteAte IS NULL OR VigenteAte >= @p_VigenteEm::date));
            """,
            new
            {
                p_Nome = nome?.ToLower(),
                p_Sigla = sigla?.ToLower(),
                p_Aka = aka?.ToLower(),
                p_Habilitado = habilitado,
                p_VigenteEm = vigenteEm?.ToString("yyyy-MM-dd HH:mm:ss")
            });
    }

    public async Task IncluirAplicacao(IAplicacao aplicacao)
    {
        await _uow.DbConnection.ExecuteAsync(
            """
            INSERT INTO Aplicacoes (AppId, Nome, Sigla, Aka, Habilitado, VigenteDe, VigenteAte)
            VALUES (@AppId, @Nome, @Sigla, @Aka, @Habilitado, @VigenteDe, @VigenteAte)
            """,
            aplicacao);
    }

    public async Task<IAplicacao?> BuscarAplicacaoPorId(
        Guid appId)
    {
        return (await _uow.DbConnection.QueryAsync<Aplicacao>(
                """
                SELECT *
                FROM Aplicacoes
                WHERE
                    AppId = @p_AppId::uuid;
                """,
                new
                {
                    p_AppId = appId
                }))
            .SingleOrDefault<IAplicacao>();
    }

    public async Task<IAplicacao?> BuscarAplicacaoPorNome(
        string nome)
    {
        return (await _uow.DbConnection.QueryAsync<Aplicacao>(
                """
                SELECT *
                FROM Aplicacoes
                WHERE
                    LOWER(Nome) = @p_Nome;
                """,
                new
                {
                    p_Nome = nome.ToLower()
                }))
            .SingleOrDefault<IAplicacao>();
    }

    public async Task<IAplicacao?> BuscarAplicacaoPorSigla(
        string sigla)
    {
        return (await _uow.DbConnection.QueryAsync<Aplicacao>(
                """
                SELECT *
                FROM Aplicacoes
                WHERE
                    LOWER(Sigla) = @p_Sigla;
                """,
                new
                {
                    p_Sigla = sigla.ToLower()
                }))
            .SingleOrDefault<IAplicacao>();
    }

    public async Task AtualizarAplicacao(IAplicacao aplicacao)
    {
        await _uow.DbConnection.ExecuteAsync(
            """
            UPDATE Aplicacoes
            SET
                Nome = @Nome,
                Sigla = @Sigla,
                Aka = @Aka,
                Habilitado = @Habilitado,
                VigenteDe = @VigenteDe,
                VigenteAte = @VigenteAte
            WHERE AppId = @AppId;
            """,
            aplicacao);
    }

    public async Task ExcluirAplicacao(Guid appId)
    {
        await _uow.DbConnection.ExecuteAsync(
            """
            DELETE FROM Aplicacoes
            WHERE AppId = @AppId;
            """,
            new
            {
                appId
            });
    }
}

file record Aplicacao : IAplicacao
{
    public Guid AppId { get; set; }
    public required string Nome { get; set; }
    public required string Sigla { get; set; }
    public string? Aka { get; set; }
    public bool Habilitado { get; set; }
    public DateTime VigenteDe { get; set; }
    public DateTime VigenteAte { get; set; }
}
