namespace Fedatto.ConfigProvider.Domain.Valor;

public interface IValorRepository
{
    Task<IEnumerable<IValor<object>>> BuscarValoresDouble(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresString(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresBool(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresDateTime(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresJson(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresBinaryB64(
        int chaveId,
        DateTime vigenteEm,
        bool habilitado);
}
