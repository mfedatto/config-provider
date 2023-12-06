using Fedatto.HttpExceptions;
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
        [FromQuery(Name = ArgumentosNomeados.Nome)] string? nome = null,
        [FromQuery(Name = ArgumentosNomeados.Sigla)] string? sigla = null,
        [FromQuery(Name = ArgumentosNomeados.Aka)] string? aka = null,
        [FromQuery(Name = ArgumentosNomeados.Habilitado)] bool? habilitado = null,
        [FromQuery(Name = ArgumentosNomeados.VigenteEm)] DateTime? vigenteEm = null,
        [FromQuery(Name = ArgumentosNomeados.Skip)] int? skip = 0,
        [FromQuery(Name = ArgumentosNomeados.Limit)] int? limit = null)
    {
        if (vigenteEm.HasValue)
        {
            Response.Headers.Append(CabecalhosNomeados.VigenteEm, vigenteEm.Value.ToString("yyyy-MM-dd"));
        }

        return Ok((await _application.BuscarAplicacoes(
            nome,
            sigla,
            aka,
            habilitado,
            vigenteEm,
            skip,
            limit))
            .Map(aplicacao => aplicacao.ToGetResponseModel()));
    }
    
    [HttpPost(Rotas.AplicacoesPostAplicacao)]
    public async Task<ActionResult<PostAplicacaoResponseModel>> Post_Index(
        [FromBody] PostAplicacaoRequestModel requestModel)
    {
        IAplicacao aplicacao = _factory.ToEntity(requestModel);
        
        await _application.IncluirAplicacao(aplicacao);
        
        return Ok(aplicacao.ToPostResponseModel());
    }
    
    [HttpGet(Rotas.AplicacoesGetAplicacao)]
    public async Task<ActionResult<GetAplicacaoResponseModel>> Get_ById(
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId)
    {
        return Ok((await _application.BuscarAplicacaoPorId(appId))
            .ToGetResponseModel());
    }
    
    [HttpPut(Rotas.AplicacoesPutAplicacao)]
    public async Task<ActionResult> Put_ById(
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId,
        [FromBody] PutAplicacaoRequestModel requestModel)
    {
        if ((await _application.BuscarAplicacaoPorId(appId).ConfigureAwait(false)) is null)
            throw new AplicacaoNaoEncontradaException();
        
        IAplicacao aplicacao = _factory.ToEntity(requestModel, appId);
        
        await _application.AtualizarAplicacao(aplicacao);
        
        return Ok();
    }
    
    [HttpDelete(Rotas.AplicacoesDeleteAplicacao)]
    public async Task<ActionResult> Delete_ById(
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId)
    {
        if ((await _application.BuscarAplicacaoPorId(appId).ConfigureAwait(false)) is null)
            throw new AplicacaoNaoEncontradaException();
        
        await _application.ExcluirAplicacao(appId);
        
        return Ok();
    }
    
    [HttpHead(Rotas.AplicacoesHeadAplicacao)]
    public async Task<ActionResult> Head_ById(
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId)
    {
        if ((await _application.BuscarAplicacaoPorId(appId).ConfigureAwait(false)) is null)
            throw new AplicacaoNaoEncontradaException();
        
        return Ok();
    }
}
