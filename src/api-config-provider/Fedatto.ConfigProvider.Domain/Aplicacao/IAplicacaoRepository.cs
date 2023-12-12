namespace Fedatto.ConfigProvider.Domain.Aplicacao;

public interface IAplicacaoRepository
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
    Task<IAplicacao?> BuscarAplicacaoPorId(
        CancellationToken cancellationToken,
        Guid appId);
    Task<IAplicacao?> BuscarAplicacaoPorNome(
        CancellationToken cancellationToken,
        string nome);
    Task<IAplicacao?> BuscarAplicacaoPorSigla(
        CancellationToken cancellationToken,
        string sigla);
    Task AtualizarAplicacao(
        CancellationToken cancellationToken,
        IAplicacao aplicacao);
    Task ExcluirAplicacao(
        CancellationToken cancellationToken,
        Guid appId);
}
