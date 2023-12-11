namespace Fedatto.ConfigProvider.WebApi.Controllers.Chave;

public class PutChaveRequestModel
{
    public int Id { get; init; }
    public Guid AppId { get; init; }
    public required string Nome { get; init; }
    public int IdTipo { get; init; }
    public bool Lista { get; init; }
    public bool PermiteNulo { get; init; }
    public int? IdChavePai { get; init; }
    public bool Habilitado { get; init; }
    public DateTime VigenteDe { get; init; }
    public DateTime VigenteAte { get; init; }
}
