using Dapper;
using Fedatto.ConfigProvider.Domain.Exceptions;
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
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado,
        string nomeTabela)
    {
        cancellationToken.ThrowIfClientClosedRequest();
        
        return await _uow.DbConnection.QueryAsync<IValor<T>>(
            $"""
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
