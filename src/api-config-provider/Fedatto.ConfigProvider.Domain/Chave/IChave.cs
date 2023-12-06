namespace Fedatto.ConfigProvider.Domain.Chave;

public interface IChave
{
    int Id { get; }
    Guid AppId { get; }
    string Nome { get; }
    int IdTipo { get; }
    bool Lista { get; }
    bool PermiteNulo { get; }
    int? IdChavePai { get; }
    bool Habilitado { get; }
    DateTime? VigenteDe { get; }
    DateTime? VigenteAte { get; }
}
