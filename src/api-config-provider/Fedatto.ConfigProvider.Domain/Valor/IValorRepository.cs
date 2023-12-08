namespace Fedatto.ConfigProvider.Domain.Valor;

public interface IValorRepository
{
    Task<IEnumerable<IValor<object>>> BuscarValoresDouble(
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresString(
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresBool(
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresDatas(
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresJson(
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
    Task<IEnumerable<IValor<object>>> BuscarValoresBinaryB64(
        int idChave,
        DateTime vigenteEm,
        bool habilitado);
}
