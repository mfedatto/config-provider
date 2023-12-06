namespace Fedatto.ConfigProvider.Domain.Tipo;

public interface ITipo
{
    int Id { get; }
    string Nome { get; }
    bool Habilitado { get; }
}
