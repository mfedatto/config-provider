using Dapper;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.MainDbContext;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext.Repositories;

public class TipoRepository : ITipoRepository
{
    private readonly IUnitOfWork _uow;

    public TipoRepository(
        IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<IEnumerable<ITipo>> BuscarTipos(
        CancellationToken cancellationToken,
        int? id = null,
        string? nome = null,
        bool? habilitado = null)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return await _uow.DbConnection.QueryAsync<Tipo>(
            """
            SELECT *
            FROM Tipos
            WHERE
                (@Id IS NULL OR Id = @Id) AND
                (@Nome IS NULL OR LOWER(Nome) ~ @Nome) AND
                (@Habilitado IS NULL OR Habilitado = @Habilitado)
            ORDER BY Id;
            """,
            new
            {
                Id = id,
                Nome = nome?.ToLower(),
                Habilitado = habilitado
            });
    }

    public async Task<int> ContarTipos(
        CancellationToken cancellationToken,
        int? id = null,
        string? nome = null,
        bool? habilitado = null)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return await _uow.DbConnection.ExecuteScalarAsync<int>(
            """
            SELECT COUNT(*)
            FROM Tipos
            WHERE
                (@Id IS NULL OR Id = @Id) AND
                (@Nome IS NULL OR LOWER(Nome) ~ @Nome) AND
                (@Habilitado IS NULL OR Habilitado = @Habilitado);
            """,
            new
            {
                Id = id,
                Nome = nome?.ToLower(),
                Habilitado = habilitado
            });
    }
    
    public async Task<ITipo?> BuscarTipo(
        CancellationToken cancellationToken,
        int id)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        return (await _uow.DbConnection.QueryAsync<Tipo>(
            """
            SELECT *
            FROM Tipos
            WHERE
                Id = @Id;
            """,
            new
            {
                Id = id
            }))
            .SingleOrDefault<ITipo>()!;
    }
}

file record Tipo : ITipo
{
    public int Id { get; init; }
    public required string Nome { get; init; }
    public bool Habilitado { get; init; }
}
