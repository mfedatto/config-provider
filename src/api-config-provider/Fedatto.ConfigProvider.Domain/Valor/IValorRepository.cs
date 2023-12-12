namespace Fedatto.ConfigProvider.Domain.Valor;

public interface IValorRepository
{
    Task<IEnumerable<IValor<object>>> BuscarValoresDouble(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresString(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresBool(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresDatas(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresJson(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresBinaryB64(
        CancellationToken cancellationToken,
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
}
