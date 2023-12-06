namespace Fedatto.ConfigProvider.WebApi.Controllers.Tipo;

public struct GetTipoResponseModel
{
    public int Id { get; init; }
    public required string Nome { get; init; }
    public bool Habilitado { get; init; }
}
