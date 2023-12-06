namespace Fedatto.ConfigProvider.Domain.Aplicacao;

public interface IAplicacao
{
    Guid AppId { get; }
    string Nome { get; }
    string Sigla { get; }
    string? Aka { get; }
    bool Habilitado { get; }
    DateTime VigenteDe { get; }
    DateTime VigenteAte { get; }
}
