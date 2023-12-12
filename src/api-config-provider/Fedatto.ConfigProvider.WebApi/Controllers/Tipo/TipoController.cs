using Fedatto.ConfigProvider.Domain.Exceptions;
using Fedatto.ConfigProvider.Domain.Tipo;
using Fedatto.ConfigProvider.Domain.Wrappers;
using Fedatto.ConfigProvider.WebApi.Constants;
using Fedatto.ConfigProvider.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Fedatto.ConfigProvider.WebApi.Controllers.Tipo;

[Route(Rotas.Tipos)]
public class TipoController : Controller
{
    private readonly ITipoApplication _application;

    public TipoController(
        ITipoApplication application)
    {
        _application = application;
    }

    [HttpGet(Rotas.TiposGetTipos)]
    public async Task<ActionResult<IEnumerable<GetTipoResponseModel>>> Get_Index(
        CancellationToken cancellationToken,
        [FromQuery(Name = ArgumentosNomeados.IdTipo)] int? id = null,
        [FromQuery(Name = ArgumentosNomeados.Nome)] string? nome = null,
        [FromQuery(Name = ArgumentosNomeados.Habilitado)] bool? habilitado = null)
    {
        return (await _application.BuscarTipos(
                    cancellationToken,
                    id,
                    nome,
                    habilitado)
                .ConfigureAwait(false))
            .Select(tipo => tipo.ToGetResponseModel())
            .HttpOk();
    }

    [HttpGet(Rotas.TiposGetTipo)]
    public async Task<ActionResult<GetTipoResponseModel>> Get_ById(
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.IdTipo)] int id)
    {
        return (await _application.BuscarTipoPorId(
                    cancellationToken,
                    id)
                .ConfigureAwait(false))
            .ToGetResponseModel()
            .HttpOk();
    }

    [HttpHead(Rotas.TiposHeadTipo)]
    public async Task<ActionResult> Head_ById(
        CancellationToken cancellationToken,
        [FromRoute(Name = ArgumentosNomeados.IdTipo)] int id)
    {
        await _application.BuscarTipoPorId(
                cancellationToken,
                id)
            .ThenThrowIfNull<ITipo, TipoNaoEncontradoException>()
            .ConfigureAwait(false);

        return Ok();
    }
}
