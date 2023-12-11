using Fedatto.HttpExceptions;
using Dapper;
using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.MainDbContext;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext.Repositories;

public class ChaveRepository : IChaveRepository
{
    private readonly IUnitOfWork _uow;
    private readonly ChaveFactory _factory;
    private readonly ITipoRepository _tipoRepository;

    public ChaveRepository(
        IUnitOfWork uow,
        ChaveFactory factory,
        ITipoRepository tipoRepository)
    {
        _uow = uow;
        _factory = factory;
        _tipoRepository = tipoRepository;
    }

    public async Task<IEnumerable<IChave>> BuscarChaves(
        IAplicacao aplicacao,
        DateTime vigenteEm,
        string? nome = null,
        ITipo? tipo = null,
        bool? lista = null,
        bool? permiteNulo = null,
        int? idChavePai = null,
        bool habilitado = true,
        int? skip = 0,
        int? limit = null)
    {
        return (await _uow.DbConnection.QueryAsync<Chave>(
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
                OFFSET @p_Skip
                LIMIT @p_Limit;
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
                }))
            .Select(chaveEncontrada
                => _factory.Create(
                    chaveEncontrada,
                    aplicacao,
                    tipo ?? _tipoRepository.BuscarTipo(chaveEncontrada.IdTipo).Result!));
    }

    public async Task<int> ContarChaves(
        IAplicacao aplicacao,
        DateTime vigenteEm,
        string? nome = null,
        ITipo? tipo = null,
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
            });
    }

    public async Task<IChave> BuscarChavePorId(
        IAplicacao aplicacao,
        int id)
    {
        return (await _uow.DbConnection.QueryAsync<Chave>(
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
                }))
            .Select(chaveEncontrada
                => _factory.Create(
                    chaveEncontrada,
                    aplicacao,
                    _tipoRepository.BuscarTipo(chaveEncontrada.IdTipo).Result!))
            .SingleOrDefault()!;
    }

    public async Task<IChave> IncluirChave(
        IChave chaveAIncluir)
    {
        return (await _uow.DbConnection.QueryAsync<Chave>(
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
                }))
            .Select(chaveIncluida
                => _factory.Create(
                    chaveIncluida,
                    chaveAIncluir.Aplicacao,
                    chaveAIncluir.Tipo))
            .Single();
    }

    public async Task<IChave> AlterarChave(
        IChave chaveAAlterar)
    {
        return (await _uow.DbConnection.QueryAsync<Chave>(
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
                }))
            .Select(chaveAlterada =>
                _factory.Create(
                    chaveAlterada,
                    chaveAAlterar.Aplicacao,
                    chaveAAlterar.Tipo))
            .Single();
    }

    public async Task ExcluirChave(
        int idChave)
    {
        await _uow.DbConnection.ExecuteAsync(
            """
            DELETE FROM Chaves
            WHERE Id = @Id;
            """,
            new
            {
                Id = idChave
            });
    }
}

file record Chave
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
        Chave chave,
        IAplicacao aplicacao,
        ITipo tipo)
    {
        if (aplicacao is null) throw new AplicacaoNaoEncontradaException();
        if (tipo is null) throw new TipoNaoEncontradoException();

        return factory.Create(
            chave.Id,
            aplicacao,
            chave.Nome,
            tipo,
            chave.Lista,
            chave.PermiteNulo,
            chave.IdChavePai,
            chave.Habilitado,
            chave.VigenteDe,
            chave.VigenteAte);
    }
}
