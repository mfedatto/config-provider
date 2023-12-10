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
    
    public async Task<IEnumerable<ITipo>> BuscarTipos(
        int? id = null,
        string? nome = null,
        bool? habilitado = null)
    {
        int total = await _service.ContarTipos(
            id,
            nome,
            habilitado);

        if (0.Equals(total)) return Enumerable.Empty<ITipo>();
        
        return await _service.BuscarTipos(
            id,
            nome,
            habilitado);
    }

    public async Task<ITipo> BuscarTipoPorId(int id)
    {
        return await _service.BuscarTipoPorId(id);
    }
}
