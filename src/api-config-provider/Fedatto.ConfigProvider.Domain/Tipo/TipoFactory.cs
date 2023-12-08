namespace Fedatto.ConfigProvider.Domain.Tipo;

public class TipoFactory
{
    public ITipo Create(
        int id,
        string nome,
        bool habilitado)
    {
        return new Tipo
        {
            Id = id,
            Nome = nome,
            Habilitado = habilitado
        };
    }
}

file struct Tipo : ITipo
{
    public int Id { get; init; }
    public required string Nome { get; init; }
    public bool Habilitado { get; init; }
}
