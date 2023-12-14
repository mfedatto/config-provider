using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.WebApi.Controllers.Chave;

public class GetChaveResponseModel
{
    public int Id { get; init; }
    public required IAplicacao Aplicacao { get; init; }
    public required string Nome { get; init; }
    public required ITipo? Tipo { get; init; }
    public bool Lista { get; init; }
    public bool PermiteNulo { get; init; }
    public int? IdChavePai { get; init; }
    public bool Habilitado { get; init; }
}
