using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Tipo;

namespace Fedatto.ConfigProvider.Domain.Chave;

public class ChaveFactory
{
    public IChave Create(
        int id,
        IAplicacao aplicacao,
        string nome,
        ITipo tipo,
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
            Aplicacao = aplicacao,
            Nome = nome,
            Tipo = tipo,
            Lista = lista,
            PermiteNulo = permiteNulo,
            IdChavePai = idChavePai,
            Habilitado = habilitado,
            VigenteDe = vigenteDe,
            VigenteAte = vigenteAte
        };
    }
}

file record Chave : IChave
{
    public int Id { get; init; }
    public required IAplicacao Aplicacao { get; init; }
    public required string Nome { get; init; }
    public required ITipo Tipo { get; init; }
    public bool Lista { get; init; }
    public bool PermiteNulo { get; init; }
    public int? IdChavePai { get; init; }
    public bool Habilitado { get; init; }
    public DateTime? VigenteDe { get; init; }
    public DateTime? VigenteAte { get; init; }
}
