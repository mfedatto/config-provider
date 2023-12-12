using Fedatto.ConfigProvider.Domain.Aplicacao;
using Fedatto.ConfigProvider.Domain.Chave;
using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Valor;
using Fedatto.ConfigProvider.WebApi.Constants;
using Fedatto.ConfigProvider.WebApi.Extensions;
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
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.AppId)] Guid appId,
        [FromRoute(Name = ArgumentosNomeados.IdChave)] int idChave,
        [FromQuery(Name = ArgumentosNomeados.VigenteEm)] DateTime? vigenteEm,
        [FromQuery(Name = ArgumentosNomeados.Habilitado)] bool habilitado = true)
    {
        DateTime vigenteEmEfetivo = vigenteEm ?? DateTime.Now;

        IAplicacao aplicacao = await _application.BuscarAplicacaoPorId(
                cancellationToken,
                appId)!
            .ThenThrowIfNull<IAplicacao, AplicacaoNaoEncontradaException>()
            .ConfigureAwait(false);
        IChave chave = await _application.ObterChavePorId(
                cancellationToken,
                aplicacao,
                idChave)!
            .ThenThrowIfNull<IChave, ChaveNaoEncontradaException>()
            .ConfigureAwait(false);

        Response.Headers.Append(CabecalhosNomeados.VigenteEm, vigenteEmEfetivo.ToString("yyyy-MM-dd"));

        return (await _application.BuscarValores(
                cancellationToken,
                chave,
                vigenteEmEfetivo,
                habilitado))
            .Select(valor => valor.ToGetResponseModel())
            .HttpOk();
    }
}
