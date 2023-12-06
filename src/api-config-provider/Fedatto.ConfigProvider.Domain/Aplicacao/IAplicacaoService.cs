namespace Fedatto.ConfigProvider.Domain.Aplicacao;

public interface IAplicacaoService
{
    Task<IEnumerable<IAplicacao>> BuscarAplicacoes(
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null,
        int? skip = 0,
        int? limit = null);
    Task<int> ContarAplicacoes(
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null);
    Task IncluirAplicacao(IAplicacao aplicacao);
    Task<IAplicacao> BuscarAplicacaoPorId(Guid appId);
    Task AtualizarAplicacao(IAplicacao aplicacao);
    Task ExcluirAplicacao(Guid appId);
}
