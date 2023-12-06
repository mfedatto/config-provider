using Fedatto.ConfigProvider.Domain.Wrappers;

namespace Fedatto.ConfigProvider.Domain.Aplicacao;

public interface IAplicacaoApplication
{
    Task<PagedListWrapper<IAplicacao>> BuscarAplicacoes(
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null,
        int? skip = 0,
        int? limit = null);
    Task IncluirAplicacao(IAplicacao aplicacao);
    Task<IAplicacao> BuscarAplicacaoPorId(Guid appId);
    Task AtualizarAplicacao(IAplicacao aplicacao);
    Task ExcluirAplicacao(Guid appId);
}
