using Fedatto.ConfigProvider.Domain.Wrappers;

namespace Fedatto.ConfigProvider.Domain.Tipo;

public interface ITipoApplication
{
    Task<IEnumerable<ITipo>> BuscarTipos(
        int? id = null,
        string? nome = null,
        bool? habilitado = null);
    Task<ITipo> BuscarTipo(int id);
}
