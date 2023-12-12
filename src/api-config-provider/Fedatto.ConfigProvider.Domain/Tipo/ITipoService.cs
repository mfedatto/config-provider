namespace Fedatto.ConfigProvider.Domain.Tipo;

public interface ITipoService
{
    Task<IEnumerable<ITipo>> BuscarTipos(
        CancellationToken cancellationToken,
        int? id = null,
        string? nome = null,
        bool? habilitado = null);
    Task<int> ContarTipos(
        CancellationToken cancellationToken,
        int? id = null,
        string? nome = null,
        bool? habilitado = null);
    Task<ITipo> BuscarTipoPorId(
        CancellationToken cancellationToken,
        int id);
}
