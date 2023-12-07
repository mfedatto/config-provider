using Dapper;
using Fedatto.ConfigProvider.Domain.MainDbContext;
using Fedatto.ConfigProvider.Domain.Valor;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext.Repositories;

public class ValorRepository : IValorRepository
{
    private readonly IUnitOfWork _uow;

    public ValorRepository(
        IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    private async Task<IEnumerable<IValor<T>>> BuscarValores<T>(
        int idChave,
        DateTime vigenteEm,
        bool habilitado,
        string nomeTabela)
    {
        return await _uow.DbConnection.QueryAsync<IValor<T>>(
            """
            SELECT *
            FROM {nomeTabela}
            WHERE
                IdChave = @p_IdChave AND
                Habilitado = @p_Habilitado AND
                (VigenteDe IS NULL OR VigenteDe <= @p_VigenteEm::date) AND
                (VigenteAte IS NULL OR VigenteAte >= @p_VigenteEm::date)
            ORDER BY Id;
            """,
            new
            {
                p_IdChave = idChave,
                p_Habilitado = habilitado,
                p_VigenteEm = vigenteEm
            });
    }

    Task<IEnumerable<IValor<object>>> BuscarValoresDouble(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresString(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresBool(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresDateTime(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresJson(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresBinaryB64(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
}
