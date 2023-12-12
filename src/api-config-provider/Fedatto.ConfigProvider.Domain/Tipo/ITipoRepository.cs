namespace Fedatto.ConfigProvider.Domain.Tipo;

public interface ITipoRepository
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
    Task<ITipo?> BuscarTipo(
        CancellationToken cancellationToken,
        int id);
}
