using Fedatto.HttpExceptions;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.Service;

public class TipoService : ITipoService
{
    private readonly ITipoRepository _repository;

    public TipoService(
        ITipoRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<ITipo>> BuscarTipos(
        CancellationToken cancellationToken,
        int? id = null,
        string? nome = null,
        bool? habilitado = null)
    {
        return await _repository.BuscarTipos(
            cancellationToken,
            id,
            nome,
            habilitado);
    }
    
    public async Task<int> ContarTipos(
        CancellationToken cancellationToken,
        int? id = null,
        string? nome = null,
        bool? habilitado = null)
    {
        return await _repository.ContarTipos(
            cancellationToken,
            id,
            nome,
            habilitado);
    }

    public async Task<ITipo> BuscarTipoPorId(
        CancellationToken cancellationToken,
        int id)
    {
        ITipo? result;
        
        try
        {
            result =  await _repository.BuscarTipo(
                cancellationToken,
                id);
            
            if (result is null) throw new TipoNaoEncontradoException();
        }
        catch (InvalidOperationException ex)
        {
            throw new MaisDeUmTipoEncontradoException(ex);
        }

        return result;
    }
}
