using System.Data.Common;
using Dapper;
using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Exceptions;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext.Repositories;

public class AplicacaoRepository : IAplicacaoRepository
{
    private readonly DbConnection _dbConnection;
    private readonly DbTransaction _dbTransaction;

    public AplicacaoRepository(
        DbConnection dbConnection,
        DbTransaction dbTransaction)
    {
        _dbConnection = dbConnection;
        _dbTransaction = dbTransaction;
    }

    public async Task<IEnumerable<IAplicacao>> BuscarAplicacoes(
        CancellationToken cancellationToken,
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null,
        int? skip = 0,
        int? limit = null)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return await _dbConnection.QueryAsync<DbAplicacao>(
            """
            SELECT *
            FROM Aplicacoes
            WHERE
                (@Nome IS NULL OR LOWER(Nome) ~ @Nome) AND
                (@Sigla IS NULL OR LOWER(Sigla) ~ @Sigla) AND
                (@Aka IS NULL OR LOWER(Aka) ~ @Aka) AND
                (@Habilitado IS NULL OR Habilitado = @Habilitado) AND
                (@VigenteEm IS NULL OR (VigenteDe IS NULL OR VigenteDe <= @VigenteEm::date) AND (VigenteAte IS NULL OR VigenteAte >= @VigenteEm::date))
            ORDER BY Nome
            OFFSET @Skip
            LIMIT @Limit;
            """,
            new
            {
                Nome = nome?.ToLower(),
                Sigla = sigla?.ToLower(),
                Aka = aka?.ToLower(),
                Habilitado = habilitado,
                VigenteEm = vigenteEm?.ToString("yyyy-MM-dd HH:mm:ss"),
                Skip = skip,
                Limit = limit
            },
            _dbTransaction);
    }

    public async Task<int> ContarAplicacoes(
        CancellationToken cancellationToken,
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return await _dbConnection.ExecuteScalarAsync<int>(
            """
            SELECT COUNT(*)
            FROM Aplicacoes
            WHERE
                (@Nome IS NULL OR LOWER(Nome) ~ @Nome) AND
                (@Sigla IS NULL OR LOWER(Sigla) ~ @Sigla) AND
                (@Aka IS NULL OR LOWER(Aka) ~ @Aka) AND
                (@Habilitado IS NULL OR Habilitado = @Habilitado) AND
                (@VigenteEm IS NULL OR (VigenteDe IS NULL OR VigenteDe <= @VigenteEm::date) AND (VigenteAte IS NULL OR VigenteAte >= @VigenteEm::date));
            """,
            new
            {
                Nome = nome?.ToLower(),
                Sigla = sigla?.ToLower(),
                Aka = aka?.ToLower(),
                Habilitado = habilitado,
                VigenteEm = vigenteEm?.ToString("yyyy-MM-dd HH:mm:ss")
            },
            _dbTransaction);
    }

    public async Task IncluirAplicacao(
        CancellationToken cancellationToken,
        IAplicacao aplicacao)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        await _dbConnection.ExecuteAsync(
            """
            INSERT INTO Aplicacoes (AppId, Nome, Sigla, Aka, Habilitado, VigenteDe, VigenteAte)
            VALUES (@AppId, @Nome, @Sigla, @Aka, @Habilitado, @VigenteDe, @VigenteAte)
            RETURNING *;
            """,
            aplicacao,
            _dbTransaction);
    }

    public async Task<IAplicacao?> BuscarAplicacaoPorId(
        CancellationToken cancellationToken,
        Guid appId)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return (await _dbConnection.QueryAsync<DbAplicacao>(
                """
                SELECT *
                FROM Aplicacoes
                WHERE
                    AppId = @AppId::uuid;
                """,
                new
                {
                    AppId = appId
                },
                _dbTransaction))
            .SingleOrDefault<IAplicacao>();
    }

    public async Task<IAplicacao?> BuscarAplicacaoPorNome(
        CancellationToken cancellationToken,
        string nome)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return (await _dbConnection.QueryAsync<DbAplicacao>(
                """
                SELECT *
                FROM Aplicacoes
                WHERE
                    LOWER(Nome) = @Nome;
                """,
                new
                {
                    Nome = nome.ToLower()
                },
                _dbTransaction))
            .SingleOrDefault<IAplicacao>();
    }

    public async Task<IAplicacao?> BuscarAplicacaoPorSigla(
        CancellationToken cancellationToken,
        string sigla)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return (await _dbConnection.QueryAsync<DbAplicacao>(
                """
                SELECT *
                FROM Aplicacoes
                WHERE
                    LOWER(Sigla) = @Sigla;
                """,
                new
                {
                    Sigla = sigla.ToLower()
                },
                _dbTransaction))
            .SingleOrDefault<IAplicacao>();
    }

    public async Task AtualizarAplicacao(
        CancellationToken cancellationToken,
        IAplicacao aplicacao)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        await _dbConnection.ExecuteAsync(
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
            aplicacao,
            _dbTransaction);
    }

    public async Task ExcluirAplicacao(
        CancellationToken cancellationToken,
        Guid appId)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        await _dbConnection.ExecuteAsync(
            """
            DELETE FROM Aplicacoes
            WHERE AppId = @AppId;
            """,
            new
            {
                AppId = appId
            },
            _dbTransaction);
    }
}

file record DbAplicacao : IAplicacao
{
    public Guid AppId { get; init; }
    public required string Nome { get; init; }
    public required string Sigla { get; init; }
    public string? Aka { get; init; }
    public bool Habilitado { get; init; }
    public DateTime VigenteDe { get; init; }
    public DateTime VigenteAte { get; init; }
}
