using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.HttpExceptions;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Tipo;

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
        return await _repository.BuscarChaves(
            aplicacao,
            vigenteEm,
            nome,
            tipo,
            lista,
            permiteNulo,
            idChavePai,
            habilitado,
            skip,
            limit);
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
        return await _repository.ContarChaves(
            aplicacao,
            vigenteEm,
            nome,
            tipo,
            lista,
            permiteNulo,
            idChavePai,
            habilitado);
    }

    public async Task<IChave> BuscarChavePorId(
        IAplicacao aplicacao,
        int id,
        DateTime vigenteEm)
    {
        IChave? result;
        
        try
        {
            result =  await _repository.BuscarChavePorId(
                aplicacao,
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
    
    public async Task<IChave> IncluirChave(
        IChave chave)
    {
        return await _repository.IncluirChave(chave);
    }
}
