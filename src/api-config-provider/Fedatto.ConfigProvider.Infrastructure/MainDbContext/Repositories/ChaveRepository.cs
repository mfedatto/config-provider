using System.Data.Common;
using Dapper;
using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext.Repositories;

public class ChaveRepository : IChaveRepository
{
    private readonly DbConnection _dbConnection;
    private readonly DbTransaction _dbTransaction;
    private readonly ChaveFactory _factory;

    public ChaveRepository(
        DbConnection dbConnection,
        DbTransaction dbTransaction,
        ChaveFactory factory)
    {
        _dbConnection = dbConnection;
        _dbTransaction = dbTransaction;
        _factory = factory;
    }

    public async Task<IEnumerable<IChave>> BuscarChaves(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        DateTime vigenteEm,
        Func<CancellationToken, int, ITipo> buscarTipo,
        string? nome = null,
        ITipo? tipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true,
        int? skip = 0,
        int? limit = null)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return (await _dbConnection.QueryAsync<DbChave>(
                """
                SELECT *
                FROM Chaves
                WHERE
                    (AppId = @AppId::uuid) AND
                    (@Nome IS NULL OR LOWER(Nome) ~ @Nome) AND
                    (@IdTipo IS NULL OR IdTipo = @IdTipo) AND
                    (@Lista IS NULL OR Lista = @Lista) AND
                    (@PermiteNulo IS NULL OR PermiteNulo = @PermiteNulo) AND
                    (@IdTipo IS NULL OR IdTipo = @IdTipo) AND
                    (Habilitado = @Habilitado) AND
                    (VigenteDe IS NULL OR VigenteDe <= @VigenteEm::date) AND
                    (VigenteAte IS NULL OR VigenteAte >= @VigenteEm::date)
                ORDER BY Nome
                OFFSET @Skip
                LIMIT @Limit;
                """,
                new
                {
                    aplicacao.AppId,
                    VigenteEm = vigenteEm,
                    Nome = nome?.ToLower(),
                    IdTipo = tipo?.Id,
                    Lista = lista,
                    PermiteNulo = permiteNulo,
                    IdChavePai = idChavePai,
                    Habilitado = habilitado,
                    Skip = skip,
                    Limit = limit
                },
                transaction: _dbTransaction))
            .Select(chaveEncontrada
                => _factory.Create(
                    chaveEncontrada,
                    aplicacao,
                    tipo ?? buscarTipo(
                        cancellationToken,
                        chaveEncontrada.IdTipo)));
    }

    public async Task<int> ContarChaves(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        DateTime vigenteEm,
        string? nome = null,
        ITipo? tipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return await _dbConnection.ExecuteScalarAsync<int>(
            """
            SELECT COUNT(*)
            FROM Chaves
            WHERE
                (AppId = @AppId::uuid) AND
                (@Nome IS NULL OR LOWER(Nome) ~ @Nome) AND
                (@IdTipo IS NULL OR IdTipo = @IdTipo) AND
                (@Lista IS NULL OR Lista = @Lista) AND
                (@PermiteNulo IS NULL OR PermiteNulo = @PermiteNulo) AND
                (@IdTipo IS NULL OR IdTipo = @IdTipo) AND
                (Habilitado = @Habilitado) AND
                (VigenteDe IS NULL OR VigenteDe <= @VigenteEm::date) AND
                (VigenteAte IS NULL OR VigenteAte >= @VigenteEm::date)
            """,
            new
            {
                aplicacao.AppId,
                VigenteEm = vigenteEm,
                Nome = nome?.ToLower(),
                IdTipo = tipo?.Id,
                Lista = lista,
                PermiteNulo = permiteNulo,
                IdChavePai = idChavePai,
                Habilitado = habilitado
            },
            transaction: _dbTransaction);
    }

    public async Task<IChave> BuscarChavePorId(
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        int id,
        Func<CancellationToken, int, ITipo> buscarTipo)
    {
        if (buscarTipo is null) throw new ArgumentNullException(nameof(buscarTipo));
        cancellationToken.ThrowIfClientClosedRequest();

        return (await _dbConnection.QueryAsync<DbChave>(
                """
                SELECT *
                FROM Chaves
                WHERE
                    AppId = @AppId::uuid AND
                    Id = @Id;
                """,
                new
                {
                    aplicacao.AppId,
                    Id = id
                },
                transaction: _dbTransaction))
            .Select(chaveEncontrada
                => _factory.Create(
                    chaveEncontrada,
                    aplicacao,
                    buscarTipo(
                        cancellationToken,
                        chaveEncontrada.IdTipo)))
            .SingleOrDefault()!;
    }

    public async Task<IChave> IncluirChave(
        CancellationToken cancellationToken,
        IChave chaveAIncluir)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return (await _dbConnection.QueryAsync<DbChave>(
                """
                INSERT INTO Chaves (AppId, Nome, IdTipo, Lista, PermiteNulo, IdChavePai, Habilitado, VigenteDe, VigenteAte)
                VALUES (@AppId::uuid, @Nome, @IdTipo, @Lista, @PermiteNulo, @IdChavePai, @Habilitado, @VigenteDe::date, @VigenteAte::date)
                RETURNING *;
                """,
                new
                {
                    chaveAIncluir.Aplicacao.AppId,
                    chaveAIncluir.Nome,
                    IdTipo = chaveAIncluir.Tipo.Id,
                    chaveAIncluir.Lista,
                    chaveAIncluir.PermiteNulo,
                    chaveAIncluir.IdChavePai,
                    chaveAIncluir.Habilitado,
                    chaveAIncluir.VigenteDe,
                    chaveAIncluir.VigenteAte
                },
                transaction: _dbTransaction))
            .Select(chaveIncluida
                => _factory.Create(
                    chaveIncluida,
                    chaveAIncluir.Aplicacao,
                    chaveAIncluir.Tipo))
            .Single();
    }

    public async Task<IChave> AlterarChave(
        CancellationToken cancellationToken,
        IChave chaveAAlterar)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return (await _dbConnection.QueryAsync<DbChave>(
                """
                UPDATE Chaves
                SET
                    AppId = @AppId::uuid,
                    Nome = @Nome,
                    IdTipo = @IdTipo,
                    Lista = @Lista,
                    PermiteNulo = @PermiteNulo,
                    IdChavePai = @IdChavePai,
                    Habilitado = @Habilitado,
                    VigenteDe = @VigenteDe::date,
                    VigenteAte = @VigenteAte::date
                WHERE Id = @Id
                RETURNING *;
                """,
                new
                {
                    chaveAAlterar.Id,
                    chaveAAlterar.Aplicacao.AppId,
                    chaveAAlterar.Nome,
                    IdTipo = chaveAAlterar.Tipo.Id,
                    chaveAAlterar.Lista,
                    chaveAAlterar.PermiteNulo,
                    chaveAAlterar.IdChavePai,
                    chaveAAlterar.Habilitado,
                    chaveAAlterar.VigenteDe,
                    chaveAAlterar.VigenteAte
                },
                transaction: _dbTransaction))
            .Select(chaveAlterada =>
                _factory.Create(
                    chaveAlterada,
                    chaveAAlterar.Aplicacao,
                    chaveAAlterar.Tipo))
            .Single();
    }

    public async Task ExcluirChave(
        CancellationToken cancellationToken,
        int idChave)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        await _dbConnection.ExecuteAsync(
            """
            DELETE FROM Chaves
            WHERE Id = @Id;
            """,
            new
            {
                Id = idChave
            },
            transaction: _dbTransaction);
    }
}

file record DbChave
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

file static class RepositoryExtensions
{
    public static IChave Create(
        this ChaveFactory factory,
        DbChave dbChave,
        IAplicacao aplicacao,
        ITipo tipo)
    {
        if (aplicacao is null) throw new AplicacaoNaoEncontradaException();
        if (tipo is null) throw new TipoNaoEncontradoException();

        return factory.Create(
            dbChave.Id,
            aplicacao,
            dbChave.Nome,
            tipo,
            dbChave.Lista,
            dbChave.PermiteNulo,
            dbChave.IdChavePai,
            dbChave.Habilitado,
            dbChave.VigenteDe,
            dbChave.VigenteAte);
    }
}
