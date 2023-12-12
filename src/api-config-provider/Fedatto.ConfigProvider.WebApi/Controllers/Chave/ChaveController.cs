using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Wrappers;
using Fedatto.ConfigProvider.WebApi.Constants;
using Fedatto.ConfigProvider.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Fedatto.ConfigProvider.WebApi.Controllers.Chave;

[Route(Rotas.Chaves)]
public class ChaveController : Controller
{
    private readonly IChaveApplication _application;
    private readonly ChaveFactory _factory;

    public ChaveController(
        IChaveApplication application,
        ChaveFactory factory)
    {
        _application = application;
        _factory = factory;
    }
    
    [HttpGet(Rotas.ChavesGetChaves)]
    public async Task<ActionResult<PagedListWrapper<GetChaveResponseModel>>> Get_Index(
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId,
        [FromQuery(Name = ArgumentosNomeados.VigenteEm)] DateTime? vigenteEm,
        [FromQuery(Name = ArgumentosNomeados.Nome)] string? nome = null,
        [FromQuery(Name = ArgumentosNomeados.IdTipo)] int? idTipo = null,
        [FromQuery(Name = ArgumentosNomeados.Lista)] bool? lista = null,
        [FromQuery(Name = ArgumentosNomeados.PermiteNulo)] bool? permiteNulo = null,
        [FromQuery(Name = ArgumentosNomeados.IdPai)] int? idChavePai = null,
        [FromQuery(Name = ArgumentosNomeados.Habilitado)] bool habilitado = true,
        [FromQuery(Name = ArgumentosNomeados.Skip)] int? skip = 0,
        [FromQuery(Name = ArgumentosNomeados.Limit)] int? limit = null)
    {
        IAplicacao aplicacao = await _application.BuscarAplicacaoPorId(
                cancellationToken,
                appId)
            .ThenThrowIfNullOrUnavailable<IAplicacao, AplicacaoNaoEncontradaException>(result => result.Habilitado)
            .ConfigureAwait(false);
        ITipo? tipo = idTipo is null
            ? null
            : await _application.BuscarTipoPorId(
                    cancellationToken,
                    idTipo.Value)
                .ThenThrowIfNullOrUnavailable<ITipo, TipoNaoEncontradoException>(result => result.Habilitado)
                .ConfigureAwait(false);

        vigenteEm = vigenteEm ?? DateTime.Now;
        
        Response.Headers.Append(CabecalhosNomeados.VigenteEm, vigenteEm.Value.ToString("yyyy-MM-dd"));
        
        return (await _application.BuscarChaves(
                cancellationToken,
                aplicacao,
                vigenteEm.Value,
                nome,
                tipo,
                lista,
                permiteNulo,
                idChavePai,
                habilitado,
                skip,
                limit))
            .Map(chave => chave.ToGetResponseModel())
            .HttpOk();
    }
    
    [HttpPost(Rotas.ChavesPostChave)]
    public async Task<ActionResult<PostChaveResponseModel>> Post_Index(
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId,
        [FromBody] PostChaveRequestModel requestModel)
    {
        IAplicacao aplicacao = await _application.BuscarAplicacaoPorId(
                cancellationToken,
                appId)
            .ThenThrowIfNullOrUnavailable<IAplicacao, AplicacaoNaoEncontradaException>(result => result.Habilitado)
            .ConfigureAwait(false);
        ITipo tipo = await _application.BuscarTipoPorId(
                cancellationToken,
                requestModel.IdTipo)
            .ThenThrowIfNullOrUnavailable<ITipo, TipoNaoEncontradoException>(result => result.Habilitado)
            .ConfigureAwait(false);
        
        IChave chave = _factory.ToEntity(
            requestModel,
            aplicacao,
            tipo);
        
        return (await _application.IncluirChave(
                cancellationToken,
                chave))
            .ToPostResponseModel()
            .HttpOk();
    }
    
    [HttpGet(Rotas.ChavesGetChave)]
    public async Task<ActionResult<GetChaveResponseModel>> Get_ById(
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId,
        [FromRoute(Name = ArgumentosNomeados.IdChave)] int id)
    {
        IAplicacao aplicacao = await _application.BuscarAplicacaoPorId(
                cancellationToken,
                appId)
            .ThenThrowIfNullOrUnavailable<IAplicacao, AplicacaoNaoEncontradaException>(result => result.Habilitado)
            .ConfigureAwait(false);
        
        return (await _application.BuscarChavePorId(
                cancellationToken,
                aplicacao,
                id))
            .ToGetResponseModel()
            .HttpOk();
    }
    
    [HttpPut(Rotas.ChavesPostChave)]
    public async Task<ActionResult<PutChaveResponseModel>> Put_Index(
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId,
        [FromBody] PutChaveRequestModel requestModel)
    {
        await _application.BuscarAplicacaoPorId(
                cancellationToken,
                appId)
            .ThenThrowIfNullOrUnavailable<IAplicacao, AplicacaoNaoEncontradaException>(result => result.Habilitado)
            .ConfigureAwait(false);
        IAplicacao aplicacao = await _application.BuscarAplicacaoPorId(
                cancellationToken,
                requestModel.AppId)
            .ThenThrowIfNullOrUnavailable<IAplicacao, AplicacaoNaoEncontradaException>(result => result.Habilitado)
            .ConfigureAwait(false);
        ITipo tipo = await _application.BuscarTipoPorId(
                cancellationToken,
                requestModel.IdTipo)
            .ThenThrowIfNullOrUnavailable<ITipo, TipoNaoEncontradoException>(result => result.Habilitado)
            .ConfigureAwait(false);

        IChave chave = _factory.ToEntity(
            requestModel,
            aplicacao,
            tipo);
        
        return (await _application.AlterarChave(
                cancellationToken,
                chave))
            .ToPutResponseModel()
            .HttpOk();
    }
    
    [HttpDelete(Rotas.ChavesDeleteChave)]
    public async Task<ActionResult> Delete_ById(
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId,
        [FromRoute(Name = ArgumentosNomeados.IdChave)] int idChave)
    {
        IAplicacao aplicacao = await _application.BuscarAplicacaoPorId(
                cancellationToken,
                appId)
            .ThenThrowIfNullOrUnavailable<IAplicacao, AplicacaoNaoEncontradaException>(result => result.Habilitado)
            .ConfigureAwait(false);
        await _application.BuscarChavePorId(
                cancellationToken,
                aplicacao,
                idChave)
            .ThenThrowIfNull<IChave, ChaveNaoEncontradaException>()
            .ConfigureAwait(false);

        await _application.ExcluirChave(
            cancellationToken,
            idChave);
        
        return Ok();
    }
}
