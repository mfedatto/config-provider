using Fedatto.ConfigProvider.Domain.Valor;

namespace Fedatto.ConfigProvider.WebApi.Controllers.Valor;

public struct GetValorResponseModel<T>
{
    public int Id { get; init; }
    public int IdChave { get; init; }
    public T Valor { get; init; }
    public bool Habilitado { get; init; }
    public DateTime VigenteDe { get; init; }
    public DateTime VigenteAte { get; init; }
}
