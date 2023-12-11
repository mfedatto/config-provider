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
        CancellationToken cancellationToken,
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
            cancellationToken,
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
        return await _repository.ContarChaves(
            cancellationToken,
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
        CancellationToken cancellationToken,
        IAplicacao aplicacao,
        int id)
    {
        IChave? result;
        
        try
        {
            result =  await _repository.BuscarChavePorId(
                cancellationToken,
                aplicacao,
                id);
            
            if (result is null) throw new ChaveNaoEncontradaException();
        }
        catch (InvalidOperationException ex)
        {
            throw new MaisDeUmaChaveEncontradaException(ex);
        }

        return result;
    }
    
    public async Task<IChave> IncluirChave(
        CancellationToken cancellationToken,
        IChave chave)
    {
        return await _repository.IncluirChave(
            cancellationToken,
            chave);
    }

    public async Task<IChave> AlterarChave(
        CancellationToken cancellationToken,
        IChave chaveAAlterar)
    {
        return await _repository.AlterarChave(
            cancellationToken,
            chaveAAlterar);
    }

    public async Task ExcluirChave(
        CancellationToken cancellationToken,
        int idChave)
    {
        await _repository.ExcluirChave(
            cancellationToken,
            idChave);
    }
}
