namespace Fedatto.ConfigProvider.Domain.Aplicacao;

public interface IAplicacaoService
{
    Task<IEnumerable<IAplicacao>> BuscarAplicacoes(
        CancellationToken cancellationToken,
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null,
        int? skip = 0,
        int? limit = null);
    Task<int> ContarAplicacoes(
        CancellationToken cancellationToken,
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null);
    Task IncluirAplicacao(
        CancellationToken cancellationToken,
        IAplicacao aplicacao);
    Task<IAplicacao> BuscarAplicacaoPorId(
        CancellationToken cancellationToken,
        Guid appId);
    Task AtualizarAplicacao(
        CancellationToken cancellationToken,
        IAplicacao aplicacao);
    Task ExcluirAplicacao(
        CancellationToken cancellationToken,
        Guid appId);
}
