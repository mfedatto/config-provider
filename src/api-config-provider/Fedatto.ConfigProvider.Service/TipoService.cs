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
        int? id = null,
        string? nome = null,
        bool? habilitado = null,
        int? skip = 0,
        int? limit = null)
    {
        return await _repository.BuscarTipos(
            id,
            nome,
            habilitado);
    }
    
    public async Task<int> ContarTipos(
        int? id = null,
        string? nome = null,
        bool? habilitado = null,
        int? skip = 0,
        int? limit = null)
    {
        return await _repository.ContarTipos(
            id,
            nome,
            habilitado);
    }
    

    public async Task<ITipo> BuscarTipo(int id)
    {
        ITipo? result;
        
        try
        {
            result =  await _repository.BuscarTipo(id);
            
            if (result is null) throw new TipoNaoEncontradoException();
        }
        catch (InvalidOperationException ex)
        {
            throw new MaisDeUmTipoEncontradoException(ex);
        }

        return result;
    }
}
