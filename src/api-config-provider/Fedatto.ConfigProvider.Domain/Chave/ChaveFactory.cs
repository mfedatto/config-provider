namespace Fedatto.ConfigProvider.Domain.Chave;

public class ChaveFactory
{
    public IChave Create(
        int id,
        Guid appId,
        string nome,
        int idTipo,
        bool lista,
        bool permiteNulo,
        int? idChavePai,
        bool habilitado,
        DateTime? vigenteDe,
        DateTime? vigenteAte)
    {
        return new Chave
        {
            Id = id,
            AppId = appId,
            Nome = nome,
            IdTipo = idTipo,
            Lista = lista,
            PermiteNulo = permiteNulo,
            IdChavePai = idChavePai,
            Habilitado = habilitado,
            VigenteDe = vigenteDe,
            VigenteAte = vigenteAte
        };
    }
}

file struct Chave : IChave
{
    public int Id { get; init; }
    public Guid AppId { get; init; }
    public string Nome { get; init; }
    public int IdTipo { get; init; }
    public bool Lista { get; init; }
    public bool PermiteNulo { get; init; }
    public int? IdChavePai { get; init; }
    public bool Habilitado { get; init; }
    public DateTime? VigenteDe { get; init; }
    public DateTime? VigenteAte { get; init; }
}
