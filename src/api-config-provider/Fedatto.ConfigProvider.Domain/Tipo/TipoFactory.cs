namespace Fedatto.ConfigProvider.Domain.Tipo;

public class TipoFactory
{
    public ITipo Create(
        int id,
        string nome,
        bool habilitado)
    {
        return new Tipo(
            id,
            nome,
            habilitado);
    }
}

file struct Tipo : ITipo
{
    public int Id { get; init; }
    public string Nome { get; init; }
    public bool Habilitado { get; init; }

    public Tipo(
        int id,
        string nome,
        bool habilitado)
    {
        Id = id;
        Nome = nome;
        Habilitado = habilitado;
    }
}
