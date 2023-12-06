namespace Fedatto.ConfigProvider.Domain.Aplicacao;

public class AplicacaoFactory
{
    public IAplicacao Create(
        Guid appid,
        string nome,
        string sigla,
        string? aka,
        bool habilitado,
        DateTime vigenteDe,
        DateTime vigenteAte)
    {
        return new Aplicacao(
            appid,
            nome,
            sigla,
            aka,
            habilitado,
            vigenteDe,
            vigenteAte
        );
    }
}

file struct Aplicacao : IAplicacao
{
    public Guid AppId { get; init; }
    public string Nome { get; init; }
    public string Sigla { get; init; }
    public string? Aka { get; init; }
    public bool Habilitado { get; init; }
    public DateTime VigenteDe { get; init; }
    public DateTime VigenteAte { get; init; }

    public Aplicacao(
        Guid appid,
        string nome,
        string sigla,
        string? aka,
        bool habilitado,
        DateTime vigenteDe,
        DateTime vigenteAte)
    {
        AppId = appid;
        Nome = nome;
        Sigla = sigla;
        Aka = aka;
        Habilitado = habilitado;
        VigenteDe = vigenteDe;
        VigenteAte = vigenteAte;
    }
}
