using System.Data.Common;
using Dapper;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Valor;

namespace Fedatto.ConfigProvider.Infrastructure.MainDbContext.Repositories;

public class ValorRepository : IValorRepository
{
    private readonly DbConnection _dbConnection;
    private readonly DbTransaction _dbTransaction;

    public ValorRepository(
        DbConnection dbConnection,
        DbTransaction dbTransaction)
    {
        _dbConnection = dbConnection;
        _dbTransaction = dbTransaction;
    }

    private async Task<IEnumerable<IValor<T>>> BuscarValores<T>(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado,
        string nomeTabela)
    {
        cancellationToken.ThrowIfClientClosedRequest();
        
        return await _dbConnection.QueryAsync<DbValor<T>>(
            $"""
             SELECT *
             FROM {nomeTabela}
             WHERE
                 IdChave = @IdChave AND
                 Habilitado = @Habilitado AND
                 (VigenteDe IS NULL OR VigenteDe <= @VigenteEm::date) AND
                 (VigenteAte IS NULL OR VigenteAte >= @VigenteEm::date)
             ORDER BY Id;
             """,
            new
            {
                IdChave = idChave,
                Habilitado = habilitado,
                VigenteEm = vigenteEm
            },
            transaction: _dbTransaction);
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValoresDouble(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return (await BuscarValores<double>(
                cancellationToken,
                idChave,
                vigenteEm,
                habilitado,
                "ValoresNumeros"))
            .Cast<IValor<object>>();
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValoresString(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return await BuscarValores<string>(
            cancellationToken,
            idChave,
            vigenteEm,
            habilitado,
            "ValoresTextos");
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValoresBool(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return (await BuscarValores<bool>(
                cancellationToken,
                idChave,
                vigenteEm,
                habilitado,
                "ValoresLogicos"))
            .Cast<IValor<object>>();
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValoresDatas(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return (await BuscarValores<DateTime>(
                cancellationToken,
                idChave,
                vigenteEm,
                habilitado,
                "ValoresDatas"))
            .Cast<IValor<object>>();
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValoresJson(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return await BuscarValores<string>(
            cancellationToken,
            idChave,
            vigenteEm,
            habilitado,
            "ValoresJson");
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValoresBinaryB64(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return await BuscarValores<string>(
            cancellationToken,
            idChave,
            vigenteEm,
            habilitado,
            "ValoresBinarioBase64");
    }
}

file record DbValor<T> : IValor<T>
{
    public int Id { get; set; }
    public int IdChave { get; set; }
    public required T Valor { get; set; }
    public bool Habilitado { get; set; }
    public DateTime VigenteDe { get; set; }
    public DateTime VigenteAte { get; set; }
}
