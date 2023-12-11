using Fedatto.ConfigProvider.Domain.Wrappers;

namespace Fedatto.ConfigProvider.Domain.Aplicacao;

public interface IAplicacaoApplication
{
    Task<PagedListWrapper<IAplicacao>> BuscarAplicacoes(
        CancellationToken cancellationToken,
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null,
        int? skip = 0,
        int? limit = null);
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
