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
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return (await BuscarValores<double>(
                idChave,
                vigenteEm,
                habilitado,
                "ValoresNumeros"))
            .Cast<IValor<object>>();
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValoresString(
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return await BuscarValores<string>(
            idChave,
            vigenteEm,
            habilitado,
            "ValoresTextos");
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValoresBool(
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return (await BuscarValores<bool>(
                idChave,
                vigenteEm,
                habilitado,
                "ValoresLogicos"))
            .Cast<IValor<object>>();
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValoresDatas(
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return (await BuscarValores<DateTime>(
                idChave,
                vigenteEm,
                habilitado,
                "ValoresDatas"))
            .Cast<IValor<object>>();
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValoresJson(
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return await BuscarValores<string>(
            idChave,
            vigenteEm,
            habilitado,
            "ValoresJson");
    }

    public async Task<IEnumerable<IValor<object>>> BuscarValoresBinaryB64(
        int idChave,
        DateTime vigenteEm,
        bool habilitado)
    {
        return await BuscarValores<string>(
            idChave,
            vigenteEm,
            habilitado,
            "ValoresBinarioBase64");
    }
}
