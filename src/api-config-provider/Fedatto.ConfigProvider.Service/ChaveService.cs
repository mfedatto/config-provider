using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.HttpExceptions;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.MainDbContext;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.Service;

public class ChaveService : IChaveService
{
    private readonly IUnitOfWork _uow;

    public ChaveService(
        IUnitOfWork uow)
    {
        _uow = uow;
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
        return await _uow.ChaveRepository.BuscarChaves(
            cancellationToken,
            aplicacao,
            vigenteEm,
            (ct, i)
                => _uow.TipoRepository.BuscarTipo(ct, i)
                    .Result,
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
        return await _uow.ChaveRepository.ContarChaves(
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
            result =  await _uow.ChaveRepository.BuscarChavePorId(
                cancellationToken,
                aplicacao,
                id,
                (ct, i)
                    => _uow.TipoRepository.BuscarTipo(ct, i)
                        .Result);
            
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
        return await _uow.ChaveRepository.IncluirChave(
            cancellationToken,
            chave);
    }

    public async Task<IChave> AlterarChave(
        CancellationToken cancellationToken,
        IChave chaveAAlterar)
    {
        return await _uow.ChaveRepository.AlterarChave(
            cancellationToken,
            chaveAAlterar);
    }

    public async Task ExcluirChave(
        CancellationToken cancellationToken,
        int idChave)
    {
        await _uow.ChaveRepository.ExcluirChave(
            cancellationToken,
            idChave);
    }
}
