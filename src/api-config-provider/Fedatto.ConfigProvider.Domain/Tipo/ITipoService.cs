namespace Fedatto.ConfigProvider.Domain.Tipo;

public interface ITipoService
{
    Task<IEnumerable<ITipo>> BuscarTipos(
        int? id = null,
        string? nome = null,
        bool? habilitado = null);
    Task<int> ContarTipos(
        int? id = null,
        string? nome = null,
        bool? habilitado = null);
    Task<ITipo> BuscarTipo(int id);
}
