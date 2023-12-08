using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Wrappers;
using Fedatto.ConfigProvider.WebApi.Constants;
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
        IAplicacao? aplicacao = await _application.BuscarAplicacaoPorId(appId);
        
        if (aplicacao is null) throw new AplicacaoNaoEncontradaException();
        
        ITipo? tipo = null;

        if (idTipo is not null)
        {
            tipo = await _application.BuscarTipoPorId(idTipo.Value);

            if (tipo is null || !tipo.Habilitado) throw new TipoNaoEncontradoException();
        }

        DateTime vigenteEmEfetivo = vigenteEm ?? DateTime.Now;
        
        Response.Headers.Append(CabecalhosNomeados.VigenteEm, vigenteEmEfetivo.ToString("yyyy-MM-dd"));
        
        return Ok((await _application.BuscarChaves(
                aplicacao,
                vigenteEmEfetivo,
                nome,
                tipo,
                lista,
                permiteNulo,
                idChavePai,
                habilitado,
                skip,
                limit))
            .Map(chave => chave.ToGetResponseModel()));
    }
    
    [HttpPost(Rotas.ChavesPostChave)]
    public async Task<ActionResult<PostChaveResponseModel>> Post_Index(
        [FromBody] PostChaveRequestModel requestModel)
    {
        IAplicacao? aplicacao = await _application.BuscarAplicacaoPorId(requestModel.AppId);
        ITipo? tipo = await _application.BuscarTipoPorId(requestModel.IdTipo);
        
        if (aplicacao is null || !aplicacao.Habilitado) throw new AplicacaoNaoEncontradaException();
        if (tipo is null || !tipo.Habilitado) throw new TipoNaoEncontradoException();

        IChave chave = _factory.ToEntity(
            requestModel,
            aplicacao,
            tipo);
        
        return Ok((await _application.IncluirChave(chave))
            .ToPostResponseModel());
    }
    
    [HttpGet(Rotas.ChavesGetChave)]
    public async Task<ActionResult<GetChaveResponseModel>> Get_ById(
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId,
        [FromRoute(Name = ArgumentosNomeados.IdChave)] int id,
        [FromQuery(Name = ArgumentosNomeados.VigenteEm)] DateTime? vigenteEm)
    {
        IAplicacao? aplicacao = await _application.BuscarAplicacaoPorId(appId);
        
        if (aplicacao is null || !aplicacao.Habilitado) throw new AplicacaoNaoEncontradaException();
        
        DateTime vigenteEmEfetivo = vigenteEm ?? DateTime.Now;
        
        Response.Headers.Append(CabecalhosNomeados.VigenteEm, vigenteEmEfetivo.ToString("yyyy-MM-dd"));
        
        return Ok((await _application.BuscarChavePorId(
                aplicacao,
                id,
                vigenteEmEfetivo))
            .ToGetResponseModel());
    }
    
    [HttpPut(Rotas.ChavesPutChave)]
    public async Task<ActionResult<PostChaveResponseModel>> Put_Index(
        [FromBody] PostChaveRequestModel requestModel)
    {
        IAplicacao? aplicacao = await _application.BuscarAplicacaoPorId(requestModel.AppId);
        ITipo? tipo = await _application.BuscarTipoPorId(requestModel.IdTipo);
        
        if (aplicacao is null || !aplicacao.Habilitado) throw new AplicacaoNaoEncontradaException();
        if (tipo is null || !tipo.Habilitado) throw new TipoNaoEncontradoException();

        IChave chave = _factory.ToEntity(
            requestModel,
            aplicacao,
            tipo);
        
        return Ok((await _application.IncluirChave(chave))
            .ToPostResponseModel());
    }
}
