using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Valor;
using Fedatto.ConfigProvider.Domain.Wrappers;
using Fedatto.ConfigProvider.WebApi.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Fedatto.ConfigProvider.WebApi.Controllers.Valor;

[Route(Rotas.Valores)]
public class ValorController : Controller
{
    private readonly IValorApplication _application;

    public ValorController(
        IValorApplication application)
    {
        _application = application;
    }
    
    [HttpGet(Rotas.ValoresGetValores)]
    public async Task<ActionResult<IEnumerable<GetValorResponseModel<object>>>> Get_Index(
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId,
        [FromRoute(Name = ArgumentosNomeados.IdChave)] int idChave,
        [FromQuery(Name = ArgumentosNomeados.VigenteEm)] DateTime? vigenteEm,
        [FromQuery(Name = ArgumentosNomeados.Habilitado)] bool habilitado = true)
    {
        IAplicacao? aplicacao = await _application.BuscarAplicacaoPorId(appId);
        
        if (aplicacao is null) throw new AplicacaoNaoEncontradaException();
        
        DateTime vigenteEmEfetivo = vigenteEm ?? DateTime.Now;
        IChave? chave = await _application.ObterChavePorId(aplicacao, idChave, vigenteEmEfetivo);
        
        if (chave is null) throw new ChaveNaoEncontradaException();
        
        Response.Headers.Append(CabecalhosNomeados.VigenteEm, vigenteEmEfetivo.ToString("yyyy-MM-dd"));
        
        return Ok((await _application.BuscarValores(
                chave,
                vigenteEmEfetivo,
                habilitado))
            .Select(valor => valor.ToGetResponseModel()));
    }
}
