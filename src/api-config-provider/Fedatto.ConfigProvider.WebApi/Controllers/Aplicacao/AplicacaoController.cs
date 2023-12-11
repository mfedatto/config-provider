using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Wrappers;
using Fedatto.ConfigProvider.WebApi.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Fedatto.ConfigProvider.WebApi.Controllers.Aplicacao;

[Route(Rotas.Aplicacoes)]
public class AplicacaoController : Controller
{
    private readonly IAplicacaoApplication _application;
    private readonly AplicacaoFactory _factory;

    public AplicacaoController(
        IAplicacaoApplication application,
        AplicacaoFactory factory)
    {
        _application = application;
        _factory = factory;
    }

    [HttpGet(Rotas.AplicacoesGetAplicacoes)]
    public async Task<ActionResult<PagedListWrapper<GetAplicacaoResponseModel>>> Get_Index(
        CancellationToken cancellationToken,
        [FromQuery(Name = ArgumentosNomeados.Nome)] string? nome = null,
        [FromQuery(Name = ArgumentosNomeados.Sigla)] string? sigla = null,
        [FromQuery(Name = ArgumentosNomeados.Aka)] string? aka = null,
        [FromQuery(Name = ArgumentosNomeados.Habilitado)] bool? habilitado = null,
        [FromQuery(Name = ArgumentosNomeados.VigenteEm)] DateTime? vigenteEm = null,
        [FromQuery(Name = ArgumentosNomeados.Skip)] int? skip = 0,
        [FromQuery(Name = ArgumentosNomeados.Limit)] int? limit = null)
    {
        vigenteEm = vigenteEm ?? DateTime.Now;

        Response.Headers.Append(CabecalhosNomeados.VigenteEm, vigenteEm.Value.ToString("yyyy-MM-dd"));

        return Ok((await _application.BuscarAplicacoes(
                    cancellationToken,
                    nome,
                    sigla,
                    aka,
                    habilitado,
                    vigenteEm,
                    skip,
                    limit)
                .ConfigureAwait(false))
            .Map(aplicacao => aplicacao.ToGetResponseModel()));
    }

    [HttpPost(Rotas.AplicacoesPostAplicacao)]
    public async Task<ActionResult<PostAplicacaoResponseModel>> Post_Index(
        CancellationToken cancellationToken,
        [FromBody] PostAplicacaoRequestModel requestModel)
    {
        IAplicacao aplicacao = _factory.ToEntity(requestModel);

        await _application.IncluirAplicacao(
            cancellationToken,
            aplicacao);

        return Ok(aplicacao.ToPostResponseModel());
    }

    [HttpGet(Rotas.AplicacoesGetAplicacao)]
    public async Task<ActionResult<GetAplicacaoResponseModel>> Get_ById(
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId)
    {
        return Ok((await _application.BuscarAplicacaoPorId(
                    cancellationToken,
                    appId)
                .ConfigureAwait(false))
            .ToGetResponseModel());
    }

    [HttpPut(Rotas.AplicacoesPutAplicacao)]
    public async Task<ActionResult> Put_ById(
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId,
        [FromBody] PutAplicacaoRequestModel requestModel)
    {
        await _application.BuscarAplicacaoPorId(
                cancellationToken,
                appId)
            .ThenThrowIfNull<IAplicacao, AplicacaoNaoEncontradaException>()
            .ConfigureAwait(false);

        IAplicacao aplicacao = _factory.ToEntity(requestModel, appId);

        await _application.AtualizarAplicacao(
            cancellationToken,
            aplicacao);

        return Ok();
    }

    [HttpDelete(Rotas.AplicacoesDeleteAplicacao)]
    public async Task<ActionResult> Delete_ById(
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId)
    {
        await _application.BuscarAplicacaoPorId(
                cancellationToken,
                appId)
            .ThenThrowIfNull<IAplicacao, AplicacaoNaoEncontradaException>()
            .ConfigureAwait(false);

        await _application.ExcluirAplicacao(
            cancellationToken,
            appId);

        return Ok();
    }

    [HttpHead(Rotas.AplicacoesHeadAplicacao)]
    public async Task<ActionResult> Head_ById(
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId)
    {
        await _application.BuscarAplicacaoPorId(
                cancellationToken,
                appId)
            .ThenThrowIfNull<IAplicacao, AplicacaoNaoEncontradaException>()
            .ConfigureAwait(false);

        return Ok();
    }
}
