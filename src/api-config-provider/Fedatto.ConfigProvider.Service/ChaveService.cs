using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.HttpExceptions;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Exceptions;

namespace Fedatto.ConfigProvider.Service;

public class ChaveService : IChaveService
{
    private readonly IChaveRepository _repository;

    public ChaveService(
        IChaveRepository repository)
    {
        _repository = repository;
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
        return await _repository.BuscarChaves(
            appId,
            vigenteEm,
            nome,
            idTipo,
            lista,
            permiteNulo,
            idChavePai,
            habilitado,
            skip,
            limit);
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
        return await _repository.ContarChaves(
            appId,
            vigenteEm,
            nome,
            idTipo,
            lista,
            permiteNulo,
            idChavePai,
            habilitado);
    }

    public async Task<IChave> BuscarChavePorId(
        Guid appId,
        int id,
        DateTime vigenteEm)
    {
        IChave? result;
        
        try
        {
            result =  await _repository.BuscarChavePorId(
                appId,
                id,
                vigenteEm);
            
            if (result is null) throw new ChaveNaoEncontradaException();
        }
        catch (InvalidOperationException ex)
        {
            throw new MaisDeUmaChaveEncontradaException(ex);
        }

        return result;
    }
}
