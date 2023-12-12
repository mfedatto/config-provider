using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Wrappers;

namespace Fedatto.ConfigProvider.Application;

public class AplicacaoApplication : IAplicacaoApplication
{
    private readonly IAplicacaoService _service;

    public AplicacaoApplication(IAplicacaoService service)
    {
        _service = service;
    }

    public async Task<PagedListWrapper<IAplicacao>> BuscarAplicacoes(
        CancellationToken cancellationToken,
        string? nome = null,
        string? sigla = null,
        string? aka = null,
        bool? habilitado = null,
        DateTime? vigenteEm = null,
        int? skip = 0,
        int? limit = null)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        int total = await _service.ContarAplicacoes(
            cancellationToken,
            nome,
            sigla,
            aka,
            habilitado,
            vigenteEm);

        if (0.Equals(total)) return Enumerable.Empty<IAplicacao>().WrapUp();

        cancellationToken.ThrowIfClientClosedRequest();

        return (await _service.BuscarAplicacoes(
                cancellationToken,
                nome,
                sigla,
                aka,
                habilitado,
                vigenteEm,
                skip,
                limit))
            .WrapUp(skip ?? 0, limit, total);
    }

    public async Task IncluirAplicacao(
        CancellationToken cancellationToken,
        IAplicacao aplicacao)
    {
        await _service.IncluirAplicacao(
            cancellationToken,
            aplicacao);
    }

    public async Task<IAplicacao> BuscarAplicacaoPorId(
        CancellationToken cancellationToken,
        Guid appId)
    {
        return await _service.BuscarAplicacaoPorId(
            cancellationToken,
            appId);
    }

    public async Task AtualizarAplicacao(
        CancellationToken cancellationToken,
        IAplicacao aplicacao)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        await _service.BuscarAplicacaoPorId(
                cancellationToken,
                aplicacao.AppId)
            .ThenThrowIfNull<IAplicacao, AplicacaoNaoEncontradaException>();

        await _service.AtualizarAplicacao(
            cancellationToken,
            aplicacao);
    }

    public async Task ExcluirAplicacao(
        CancellationToken cancellationToken,
        Guid appId)
    {
        cancellationToken.ThrowIfClientClosedRequest();

        await _service.BuscarAplicacaoPorId(
                cancellationToken,
                appId)
            .ThenThrowIfNull<IAplicacao, AplicacaoNaoEncontradaException>();

        await _service.ExcluirAplicacao(
            cancellationToken,
            appId);
    }
}
