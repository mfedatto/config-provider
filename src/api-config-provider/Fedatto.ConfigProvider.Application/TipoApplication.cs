using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Wrappers;
using Fedatto.HttpExceptions;

namespace Fedatto.ConfigProvider.Application;

public class TipoApplication : ITipoApplication
{
    private readonly ITipoService _service;

    public TipoApplication(
        ITipoService service)
    {
        _service = service;
    }
    
    public async Task<PagedListWrapper<ITipo>> BuscarTipos(
        int? id = null,
        string? nome = null,
        bool? habilitado = null,
        int? skip = 0,
        int? limit = null)
    {
        int total = await _service.ContarTipos(
            id,
            nome,
            habilitado);

        if (0.Equals(total)) return Enumerable.Empty<ITipo>().WrapUp();
        
        return (await _service.BuscarTipos(
                id,
                nome,
                habilitado,
                skip,
                limit))
            .WrapUp(skip ?? 0, limit, total);
    }

    public async Task<ITipo> BuscarTipo(int id)
    {
        return await _service.BuscarTipo(id);
    }
}
