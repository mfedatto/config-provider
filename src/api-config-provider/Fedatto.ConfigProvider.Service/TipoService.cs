using Fedatto.HttpExceptions;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.MainDbContext;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.Service;

public class TipoService : ITipoService
{
    private readonly IUnitOfWork _uow;

    public TipoService(
        IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<IEnumerable<ITipo>> BuscarTipos(
        CancellationToken cancellationToken,
        int? id = null,
        string? nome = null,
        bool? habilitado = null)
    {
        return await _uow.TipoRepository.BuscarTipos(
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
        return await _uow.TipoRepository.ContarTipos(
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
            result =  await _uow.TipoRepository.BuscarTipo(
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
